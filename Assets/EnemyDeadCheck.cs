using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadCheck : MonoBehaviour
{
    public EnemyBase Enemy;
    public GameObject Wall;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == true)
        {
            Wall.SetActive(false);
        }
    }
}
