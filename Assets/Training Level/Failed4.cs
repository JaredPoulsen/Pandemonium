using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failed4 : MonoBehaviour
{
    public EnemyBase enemy;
    public ThirdPersonController player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {

            if (enemy.dead == true && enemy.slow == false)
            {
                player.Health -= 100;
            }
        }

    }
}
