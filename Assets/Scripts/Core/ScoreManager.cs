using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "YesChef_HighScore";

    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    public bool IsNewHighScore { get; private set; }

    private void Awake()
    {
        CurrentScore = 0;

        HighScore = PlayerPrefs.GetInt(
            HighScoreKey,
            0
        );

        IsNewHighScore = false;
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;

        Debug.Log(
            $"Score: {CurrentScore} | " +
            $"High Score: {HighScore}"
        );
    }

    public void FinalizeScore()
    {
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;

            IsNewHighScore = true;

            PlayerPrefs.SetInt(
                HighScoreKey,
                HighScore
            );

            PlayerPrefs.Save();

            Debug.Log(
                $"NEW HIGH SCORE: {HighScore}"
            );
        }
        else
        {
            IsNewHighScore = false;
        }
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        IsNewHighScore = false;
    }
}