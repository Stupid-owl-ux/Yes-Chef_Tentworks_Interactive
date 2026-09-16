using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "MainScene";

    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            gameplaySceneName
        );
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        Application.Quit();

        Debug.Log("Quit Game");
    }
}