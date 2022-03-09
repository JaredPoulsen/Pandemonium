using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThrowing : MonoBehaviour
{
    private Collider collider;
    public GameObject children;
    public float waitTime = 3;
    public float destroyTime = 30;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        children.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(changeCollider), waitTime);
        Destroy(gameObject, destroyTime);
    }
    void changeCollider()
    {
        children.gameObject.SetActive(true);
        collider = GetComponentInChildren<Collider>();
    }
}
