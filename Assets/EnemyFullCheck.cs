using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFullCheck : MonoBehaviour
{
    public EnemyBase Enemy;
    public bool isFull;
    public AudioSource SuccessAudio;
    // Start is called before the first frame update
    void Start()
    {
        isFull = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.full == true)
        {
            isFull = true;
        }
        if (isFull == true)
        {
            SuccessAudio.Play();
        }
    }
}
