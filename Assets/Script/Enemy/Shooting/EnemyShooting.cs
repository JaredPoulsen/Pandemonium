using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    /// <summary>
    /// Randomly Shooting Angle
    /// Requires Ray Cast Script
    /// </summary>

    public EnemyRayCast detect;
    public GameObject bullet;

    [Range(0, 10)]
    public float inaccuracy;

    public float fireRate = 1f;
    private float nextShot;

    // Update is called once per frame
    void Update()
    {
        if (detect.canSeePlayer)
        {
            transform.LookAt(detect.playerRef.transform);
            Shoot();
        }
    }
    void Shoot()
    {
        float randomNumberX = Random.Range(-inaccuracy, inaccuracy);
        float randomNumberY = Random.Range(-inaccuracy, inaccuracy);
        float randomNumberZ = Random.Range(-inaccuracy, inaccuracy);

        if (Time.time >= nextShot)
        {
            GameObject clonebullet = Instantiate(bullet, transform.position + transform.forward * 0.5f + transform.up * 0.25f, transform.rotation) as GameObject;
            clonebullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            Destroy(clonebullet, 0.05f);
            nextShot = Time.time + fireRate;
        }
    }
}
