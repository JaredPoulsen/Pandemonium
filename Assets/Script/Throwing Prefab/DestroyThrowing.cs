using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThrowing : MonoBehaviour
{
    private Collider collider;
    public GameObject pickUpCollider;
    public float waitTime = 3;
    public float destroyTime = 30;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        pickUpCollider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(changeCollider), waitTime);
        Destroy(gameObject, destroyTime);
    }
    void changeCollider()
    {
        pickUpCollider.gameObject.SetActive(true);
        collider = GetComponentInChildren<Collider>();
    }
}
