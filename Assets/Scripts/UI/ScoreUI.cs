using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Update()
    {
        if (scoreManager == null)
            return;

        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text =
                $"Score: {scoreManager.CurrentScore}";
        }

        if (highScoreText != null)
        {
            highScoreText.text =
                $"High Score: {scoreManager.HighScore}";
        }
    }
}