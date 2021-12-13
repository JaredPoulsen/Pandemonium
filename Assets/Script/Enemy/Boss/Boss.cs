using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (canSeePlayer)
        {
            animator.SetBool("Shoot", true);
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
       // Debug.Log(health);
    }
   

}
