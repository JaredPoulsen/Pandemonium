using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyFullCheck : MonoBehaviour
{
    public EnemyBase Enemy;
    public bool isFull;
    public GameObject endSound;
    public float EndTime = 15;
    // Start is called before the first frame update
    void Start()
    {
        endSound.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(isFull);
        if (Enemy.full == true)
        {
            isFull = true;
            Debug.Log("haha");
            
        }
        if (isFull == true)
        {
            endSound.gameObject.SetActive(true);
            Invoke(nameof(changeLevel), 12  );
        }
        else
        {
            endSound.gameObject.SetActive(false);
        }
        
        
    }
 
    void changeLevel()
    {
        SceneManager.LoadScene("GameLevel_v3");
    }

}
