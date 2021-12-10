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
            //animator.SetBool("Shoot", false);
            animator.SetBool("Run", true);
        }
       // Debug.Log(health);
    }
   /* public override void Shoot()
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
            ShootAudio.Play();
            Destroy(clonebullet1, 5f);
            Destroy(clonebullet2, 5f);
            nextShot = Time.time + timeBetweenShot;
        }

    }*/

}
