using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            RB.isKinematic = false;
        }
    }
}
