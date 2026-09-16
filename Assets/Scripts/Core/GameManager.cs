using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 180f;

    [Header("UI")]
    [SerializeField] private GameOverUI gameOverUI;

    public GameState CurrentState { get; private set; }

    public float RemainingTime { get; private set; }

    public bool IsPlaying =>
        CurrentState == GameState.Playing;

    private void Awake()
    {
        Time.timeScale = 1f;

        RemainingTime = gameDuration;
        CurrentState = GameState.Playing;
    }

    private void Update()
    {
        if (CurrentState != GameState.Playing)
            return;

        UpdateGameTimer();
    }

    private void UpdateGameTimer()
    {
        RemainingTime -= Time.deltaTime;

        if (RemainingTime <= 0f)
        {
            RemainingTime = 0f;
            EndGame();
        }
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Paused;

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
            return;

        CurrentState = GameState.Playing;

        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        if (CurrentState == GameState.GameOver)
            return;

        CurrentState = GameState.GameOver;

        Time.timeScale = 0f;

        Debug.Log("GAME OVER");

        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver();
        }
    }
}