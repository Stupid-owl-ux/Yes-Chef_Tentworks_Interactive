using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreparationProgressUI : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TMP_Text timeText;

    public void UpdateProgress(float progress, float remainingTime)
    {
        progressSlider.value = progress;
        timeText.text = $"{remainingTime:0.0}s";
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}