using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThrowing : MonoBehaviour
{
    private Collider collider;
    public GameObject children;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        children.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(changeCollider), 5);
        Destroy(gameObject, 20f);
    }
    void changeCollider()
    {
        children.gameObject.SetActive(true);
        Debug.Log("changed");
        collider = GetComponentInChildren<Collider>();
    }
}
