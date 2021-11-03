using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{

    protected override void Start()
    {
        
        base.Start();
        maxHealth = 1000;
    }

    protected override void Update()
    {
        base.Update();

    }

}
