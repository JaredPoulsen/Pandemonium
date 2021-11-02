using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon : EnemyBase
{
    private float min;
    private float max;

    protected override void Start()
    {
        base.Start();
        min = transform.position.z;
        max = transform.position.z + 15;
    }

    protected override void Update()
    {
        base.Update();
        if (canSeePlayer)
        {
            transform.LookAt(playerRef.transform);

            if (Vector3.Distance(playerRef.transform.position, gameObject.transform.position) >= 7f)
            {
                moveSpeed = 0.01f;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerRef.transform.position.x, transform.position.y, playerRef.transform.position.z), moveSpeed);

            }
            else
            {
                moveSpeed = 0f;
            }
        }
        else if (!canSeePlayer)
        {
            transform.position = new Vector3(transform.position.x,  transform.position.y, Mathf.PingPong(Time.time * 2, max - min) + min);
        }
    }

}
