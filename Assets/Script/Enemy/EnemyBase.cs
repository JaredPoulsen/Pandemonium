using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource), typeof(CapsuleCollider))]
public class EnemyBase : MonoBehaviour
{
    // Calling variable here
    #region Variables
    [Header("Base Stat")]
    [SerializeField]
    protected string name;
    public float maxHealth = 100f;
    [SerializeField]
    protected int scorePoint = 10;
    public Score score;
    public float health;

    
    [Header("FOV")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    [Header("References")]
    public GameObject playerRef; // DO NOT DEFINE / DRAG ANYTHING TO THIS
    [SerializeField] protected Animator animator = null;
    private RagdollEnemyAdvanced rgd;
    public AudioSource ShootAudio;
    public NavMeshAgent navMeshAgent;

    [Header("Shooting")]
    public GameObject bullet;
    [Range(0, 2)]
    public float inaccuracy;
    public float timeBetweenShot = 0.5f; //  more value means low fire rate
    private float baseTimeBetweenShot;
    protected float nextShot;
    // Adjust shooting point
    [Range(-1, 1)]
    public float upward;
    [Range(-1, 1)]
    public float forward;
    [Range(-1, 1)]
    public float leftright;



    #endregion //Calling Var 


    protected virtual void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        baseTimeBetweenShot = timeBetweenShot;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        rgd = GetComponent<RagdollEnemyAdvanced>();
    }
    protected virtual void Update()
    {
        if (canSeePlayer && rgd.State != RagdollEnemyAdvanced.RagdollState.Ragdolled && rgd.State != RagdollEnemyAdvanced.RagdollState.WaitStablePosition)
        { 
            transform.LookAt(playerRef.transform);
            timeBetweenShot = baseTimeBetweenShot;
            Shoot();
            if (health > 0)
            {
                navMeshAgent.isStopped = true;
            }     
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
        if(health <= 0)
        {
            navMeshAgent.isStopped = true;
        }
        Physics.IgnoreLayerCollision(10, 20, true);
    }
    // Ray Cast System 
    #region RayCast
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            //for(int i = 0; i <= rangeChecks.Length; i++)
            //{
                Transform target = rangeChecks[0].transform;

                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeePlayer = false;
                    }
                    else
                    {
                        canSeePlayer = true;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            //}
           
            // If you want to check multiple objects inside the layer targetMask, do a for loop of rangeChecks
            
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }

    }
    #endregion //RayCast System

    // Shooting System
    #region Shooting
    public virtual void Shoot()
    {
        float randomNumberX = Random.Range(-inaccuracy, inaccuracy);
        float randomNumberY = Random.Range(-inaccuracy, inaccuracy);
        float randomNumberZ = Random.Range(-inaccuracy, inaccuracy);

        if (Time.time >= nextShot)
        {
            GameObject clonebullet = Instantiate(bullet, transform.position + transform.forward * forward + transform.up * upward + transform.right * leftright, transform.rotation) as GameObject;
            clonebullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            ShootAudio.Play();
            Destroy(clonebullet, 5f);
            nextShot = Time.time + timeBetweenShot;
        }
    }
    #endregion

    private void Awake()
    {
        health = maxHealth;
    }
    protected void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    protected void Die()
    {

        timeBetweenShot = 1000000;
        score.value += scorePoint;
        rgd.State = RagdollEnemyAdvanced.RagdollState.Ragdolled;  
        Destroy(gameObject, 10f);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(10f);
            canSeePlayer = true;
            radius = 35f;
           
        }
        if (collision.gameObject.tag == "Enemy")
        {
            rgd.State = RagdollEnemyAdvanced.RagdollState.Ragdolled;
            rgd.RagdollStatesController();
        }
    }
}
