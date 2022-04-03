using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRollCheck : MonoBehaviour
{
    public EnemyBase Enemy;
    public GameObject Wall;
    public bool isRolled;
    // Start is called before the first frame update
    void Start()
    {
        isRolled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.roll == true)
        {
            isRolled = true;
        }
        if (isRolled == true)
        {
            Wall.SetActive(false);
        }
    }
}
