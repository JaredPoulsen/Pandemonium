using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankAndScore_UI : MonoBehaviour
{
    public Score score;
    int PlayerScore;
    public Text ScoreText;
    public Text RankText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScore = score.value;
        ScoreText.text = "Your Score: " + PlayerScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScore == 0)
        {
            RankText.text = "Your Rank: F";
        }
        if (PlayerScore >= 1  && PlayerScore < 51)
        {
            RankText.text = "Your Rank: D";
        }
        if (PlayerScore > 50 && PlayerScore < 101)
        {
            RankText.text = "Your Rank: C";
        }
        if (PlayerScore > 100 && PlayerScore < 151)
        {
            RankText.text = "Your Rank: B";
        }
        if (PlayerScore > 150 && PlayerScore < 201)
        {
            RankText.text = "Your Rank: A";
        }
        if (PlayerScore > 200)
        {
            RankText.text = "Your Rank: S";
        }
    }
}
