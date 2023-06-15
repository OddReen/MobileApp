using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public static MenuSystem instance;

    [SerializeField] GameObject startGame;
    [SerializeField] GameObject restartGame;
    [SerializeField] GameObject gameUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        //gameUI.SetActive(true);
        //Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        restartGame.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0;
    }
}
