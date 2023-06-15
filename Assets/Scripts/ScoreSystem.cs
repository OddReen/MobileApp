using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI scoreText;

    int score;
    int highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }
    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = "Score: " + score;
    }
    public void AddHighScore(int scoreAmount)
    {
        highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);
    }
}
