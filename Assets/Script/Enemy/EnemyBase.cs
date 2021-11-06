using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Calling variable here
    #region Variables

    protected string name;

    protected float moveSpeed = 3f;

    public float maxHealth = 100f;

    [SerializeField]
    protected int scorePoint = 10;

    public Score score;

    private float health;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef; // DO NOT DEFINE / DRAG ANYTHING TO THIS

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public GameObject bullet;

    public AudioSource ShootAudio;

    [Range(0, 10)]
    public float inaccuracy;

    public float fireRate = 1f; //  more value means low fire rate
    private float nextShot;

    // Adjust shooting point
    [Range(-5, 5)]
    public float upward;
    [Range(-5, 5)]
    public float forward;
    [Range(-5, 5)]
    public float leftright;
    #endregion //Calling Var 


    protected virtual void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }
    protected virtual void Update()
    {
        if (canSeePlayer)
        {
            transform.LookAt(playerRef.transform);
            Shoot();

          
        }
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

    #region Shooting
    void Shoot()
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
            nextShot = Time.time + fireRate;
        }
    }
    #endregion

    private void Awake()
    {
        health = maxHealth;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        score.value += scorePoint;
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            takeDamage(10f);
        }
    }
    
}
