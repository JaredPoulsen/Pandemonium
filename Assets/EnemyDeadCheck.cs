using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadCheck : MonoBehaviour
{
    public EnemyBase Enemy;
    public GameObject Wall;
    public bool isDead;
    public GameObject Point;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.dead == true)
        {
            isDead = true;
        }
        if(isDead == true)
        {
            Point.SetActive(true);
            Wall.SetActive(false);
        }
    }
}
