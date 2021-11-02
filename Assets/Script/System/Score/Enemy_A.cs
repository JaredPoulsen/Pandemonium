using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_A : EnemyBase
{
    public Score score;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
       
    }
    public override void Die()
    {
        score.value += 10;
        base.Die();
    }
}
