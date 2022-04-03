using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowMoCheck : MonoBehaviour
{
    public EnemyBase Enemy;
    public GameObject Wall;
    public bool isSlowed;
    // Start is called before the first frame update
    void Start()
    {
        isSlowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.slow == true)
        {
            isSlowed = true;
        }
        if (isSlowed == true)
        {
            Wall.SetActive(false);
        }
    }
}
