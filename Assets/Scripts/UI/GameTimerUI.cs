using TMPro;
using UnityEngine;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timerText;

    private void Update()
    {
        if (gameManager == null || timerText == null)
            return;

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int totalSeconds =
            Mathf.CeilToInt(gameManager.RemainingTime);

        int minutes =
            totalSeconds / 60;

        int seconds =
            totalSeconds % 60;

        timerText.text =
            $"{minutes:00}:{seconds:00}";
    }
}