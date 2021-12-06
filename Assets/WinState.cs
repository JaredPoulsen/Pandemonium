using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour
{
    public GameObject boss; // drag the boss on the inspector 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check the boss 
        if (boss == null)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
