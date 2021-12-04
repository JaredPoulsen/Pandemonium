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
        if (canSeePlayer)
        {
            transform.LookAt(playerRef.transform);
            Shoot();
        }

    }
    public override void Shoot()
    {
        float randomNumberX = Random.Range(-inaccuracy, inaccuracy);
        float randomNumberY = Random.Range(-inaccuracy, inaccuracy);
        float randomNumberZ = Random.Range(-inaccuracy, inaccuracy);

        if (Time.time >= nextShot)
        {
            GameObject clonebullet1 = Instantiate(bullet, transform.position + transform.forward * forward + transform.up * upward + transform.right * leftright, transform.rotation) as GameObject;
            GameObject clonebullet2 = Instantiate(bullet, transform.position + transform.forward * forward + transform.up * upward + transform.right * -leftright, transform.rotation) as GameObject;
            clonebullet1.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            clonebullet2.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            if(clonebullet1 != null)
            {
                Debug.Log("boom");
            }
            ShootAudio.Play();
            Destroy(clonebullet1, 5f);
            Destroy(clonebullet2, 5f);
            nextShot = Time.time + timeBetweenShot;
        }

    }

}
