using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyFullCheck : MonoBehaviour
{
    public EnemyBase Enemy;
    public bool isFull;
    public AudioSource SuccessAudio;
    public float EndTime;
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
            EndTime -= Time.deltaTime;
        }
        if (EndTime < 0)
        {
            SceneManager.LoadScene("GameLevel_v3");
        }
    }
}
