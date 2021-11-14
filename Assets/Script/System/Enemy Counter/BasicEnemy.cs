using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator = null;

    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private NavMeshAgent NavMeshAgent;
    public Collider overall;

    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        ToggleRagdoll(false);
        overall.enabled = true;
        
    }

    private void ToggleRagdoll (bool state)
    {
        animator.enabled = !state;
        animator.SetFloat("Vel", NavMeshAgent.speed);

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }
    
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = state;
        }
    }
    private void Die()
    {
        ToggleRagdoll(true);
        NavMeshAgent.isStopped = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (health <=0)
        {
            Die();
            Destroy(gameObject, 10f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.tag == "Bullet")
         {
            health -= 10;
         }
        
    }
}
