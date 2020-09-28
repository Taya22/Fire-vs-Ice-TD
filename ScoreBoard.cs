using System;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBoard : MonoBehaviour
{
    private int Score;
    private Text ScoreText;

    private void Start()
    {
        ScoreText = GetComponent<Text>();
        ScoreText.text = Score.ToString();
    }

    public void AddScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
    }
}
