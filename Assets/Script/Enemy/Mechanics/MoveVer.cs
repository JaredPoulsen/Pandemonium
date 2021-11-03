using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVer : MonoBehaviour
{
    private float min;
    private float max;
    public EnemyBase detect;
    public float moveSpeed; // change speed in Update for if statement
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.z;
        max = transform.position.z + 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (detect.canSeePlayer)
        {
            transform.LookAt(detect.playerRef.transform);

            if (Vector3.Distance(detect.playerRef.transform.position, gameObject.transform.position) >= distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(detect.playerRef.transform.position.x, transform.position.y, detect.playerRef.transform.position.z), moveSpeed);

            }
            else
            {
                moveSpeed = 0f;
            }
        }
        else if (!detect.canSeePlayer)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * 2, max - min) + min);
        }
    }
}

