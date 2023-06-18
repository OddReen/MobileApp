using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Manager_Score : MonoBehaviour
{
    public static Manager_Score instance;

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
    public void AddHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
        }
    }
}
