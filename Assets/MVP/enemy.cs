using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Transform myTransform;
    public Transform target; 
    public int moveSpeed; 
    public int rotationSpeed; 
    public int maxdistance;

    void Awake()
    {
        myTransform = transform;
    }


    void Start()
    {
       

       
    }


    void Update()
    {
        Debug.DrawLine(target.position, myTransform.position, Color.red);


        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(target.position, myTransform.position) <= maxdistance)
        {
            //$$anonymous$$ove towards target
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            
        }
    }
}
