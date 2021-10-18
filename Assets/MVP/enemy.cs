using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] 
    // bullet
    public float cooldown;
    private float cooldownTimer;
    public GameObject bullet;
    public Transform shootingPos;

    // moving toward player
    private Transform myTransform;
    public Transform target; 
    public int moveSpeed; 
    public int rotationSpeed; 
    public int maxdistance;
    Renderer rend;
    //basic stat
    public int hp = 250;

    void Awake()
    {
        myTransform = transform;
    }


    void Start()
    {

        rend = GetComponent<Renderer>();

    }


    void Update()
    {
        Debug.DrawLine(target.position, myTransform.position, Color.red);
        Debug.Log(hp);

        // moving toward player
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(target.position, myTransform.position) <= maxdistance - 4f)
        {
            //Move towards target
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
            Shoot();

            if (Vector3.Distance(target.position, myTransform.position) <= 2f){
                myTransform.position += myTransform.forward * 0 * Time.deltaTime;
            }
                // shoot while in distance
                

        }
       
    }
    private void Shoot()
    {
        transform.LookAt(target);

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer > 0)
        {
            return;
        }
        cooldownTimer = cooldown;
        GameObject clonebullet = Instantiate(bullet, shootingPos) as GameObject;
        Destroy(clonebullet, 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);     
        }
        if (collision.gameObject.tag == "Bullet")
        {
            hp -= 10;

            rend.material.SetColor("_Color", Color.red);

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
}
