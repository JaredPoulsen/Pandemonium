using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunCheck : MonoBehaviour
{
    public EnemyBase Enemy;
    public GameObject Wall;
    public bool isStunned;
    // Start is called before the first frame update
    void Start()
    {
        isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.stun == true)
        {
            isStunned = true;
        }
        if (isStunned == true)
        {
            Wall.SetActive(false);
        }
    }
}
