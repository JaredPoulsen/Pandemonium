using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshEnemy : MonoBehaviour
{
    [SerializeField]
    public Transform destination;


    NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {


        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("This nav mesh agent componet is not attached to " + gameObject.name);
        } else
        {
            SetDestination();
        }
        
    }

    // Update is called once per frame
    void SetDestination()
    {
        if(destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }

    }
}
