using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager_Menus : MonoBehaviour
{
    public static Manager_Menus instance;

    [SerializeField] GameObject startGame;
    [SerializeField] GameObject restartGame;
    [SerializeField] GameObject gameUI;

    private void Awake()
    {
        instance ??= this;
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        startGame.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        restartGame.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        Manager_Score.instance.AddHighScore();
        restartGame.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0;
    }
}
