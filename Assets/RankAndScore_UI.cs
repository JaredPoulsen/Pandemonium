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
        if (PlayerScore >= 1  && PlayerScore < 251)
        {
            RankText.text = "Your Rank: D";
        }
        if (PlayerScore > 250 && PlayerScore < 301)
        {
            RankText.text = "Your Rank: C";
        }
        if (PlayerScore > 300 && PlayerScore < 451)
        {
            RankText.text = "Your Rank: B";
        }
        if (PlayerScore > 450 && PlayerScore < 601)
        {
            RankText.text = "Your Rank: A";
        }
        if (PlayerScore > 600)
        {
            RankText.text = "Your Rank: S";
        }
    }
}
