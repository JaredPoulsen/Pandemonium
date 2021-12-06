using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public Score score;
    public Text textScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score: " + score.value;

        //if (score.value >= 230)
        //{
          //  SceneManager.LoadScene("Win");
        //}
    }
}
