using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("ESC PRESSED");

            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (gameManager == null)
        {
            Debug.LogError(
                "PauseMenu: GameManager is not assigned."
            );

            return;
        }

        if (gameManager.CurrentState ==
            GameManager.GameState.Playing)
        {
            OpenPauseMenu();
        }
        else if (gameManager.CurrentState ==
                 GameManager.GameState.Paused)
        {
            ClosePauseMenu();
        }
    }

    private void OpenPauseMenu()
    {
        Debug.Log("Opening Pause Menu.");

        gameManager.PauseGame();

        pausePanel.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        Debug.Log("Closing Pause Menu.");

        gameManager.ResumeGame();

        pausePanel.SetActive(false);
    }

    public void Resume()
    {
        if (gameManager == null)
            return;

        if (gameManager.CurrentState ==
            GameManager.GameState.Paused)
        {
            gameManager.ResumeGame();
        }

        pausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        Application.Quit();

        Debug.Log("Quit Game");
    }
}