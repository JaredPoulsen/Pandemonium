using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    /// <summary>
    /// Fracture by part
    /// Add this script to every meshes of the fracture object
    /// </summary>
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit");
            rigidbody.isKinematic = false;
            rigidbody.WakeUp();
            Destroy(gameObject, 30f);
        }
    }

    // if you want the player breaks the object by punching, uncomment this function (Ctrl K + U)

    //private void OnTriggerEnter(Collider other)
    //{
    //    rigidbody.isKinematic = false;
    //    rigidbody.WakeUp();
    //    Destroy(gameObject, 30f);
    //}
}
