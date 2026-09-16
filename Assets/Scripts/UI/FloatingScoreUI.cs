using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float moveDistance = 1f;

    private Coroutine displayCoroutine;

    public void ShowScore(int score)
    {
        if (scoreText == null)
            return;

        // Enable the GameObject BEFORE starting the coroutine.
        gameObject.SetActive(true);

        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }

        displayCoroutine =
            StartCoroutine(
                ShowScoreCoroutine(score)
            );
    }

    private IEnumerator ShowScoreCoroutine(int score)
    {
        scoreText.text = $"+{score}";

        Vector3 startPosition =
            transform.localPosition;

        Vector3 targetPosition =
            startPosition +
            Vector3.up * moveDistance;

        Color startColor =
            scoreText.color;

        float elapsed = 0f;

        while (elapsed < displayDuration)
        {
            elapsed += Time.deltaTime;

            float progress =
                elapsed / displayDuration;

            transform.localPosition =
                Vector3.Lerp(
                    startPosition,
                    targetPosition,
                    progress
                );

            Color currentColor =
                startColor;

            currentColor.a =
                Mathf.Lerp(
                    1f,
                    0f,
                    progress
                );

            scoreText.color =
                currentColor;

            yield return null;
        }

        transform.localPosition =
            startPosition;

        scoreText.color =
            startColor;

        gameObject.SetActive(false);

        displayCoroutine = null;
    }
}