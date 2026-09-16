using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text newHighScoreText;

    private void Start()
    {
        gameOverPanel.SetActive(false);

        if (newHighScoreText != null)
        {
            newHighScoreText.gameObject.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        if (gameManager == null ||
            scoreManager == null)
        {
            return;
        }

        scoreManager.FinalizeScore();

        finalScoreText.text =
            $"Final Score: {scoreManager.CurrentScore}";

        highScoreText.text =
            $"High Score: {scoreManager.HighScore}";

        if (newHighScoreText != null)
        {
            newHighScoreText.gameObject.SetActive(
                scoreManager.IsNewHighScore
            );
        }

        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            "MainMenu"
        );
    }
}