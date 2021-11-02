using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_B : EnemyBase
{
    public Score score;
    public GameObject prefab;
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
        score.value += 50;
        base.Die();
        GameObject clone = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        Destroy(clone, 10);
    }
}
