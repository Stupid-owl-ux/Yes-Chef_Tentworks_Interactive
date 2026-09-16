using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [Header("Customer Windows")]
    [SerializeField] private CustomerWindow[] customerWindows;

    [Header("Ingredient Data")]
    [SerializeField] private IngredientData[] ingredientData;

    [Header("Order Settings")]
    [SerializeField] private float respawnDelay = 5f;

    [Header("Score")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("Game")]
    [SerializeField] private GameManager gameManager;

    public int ActiveOrderCount
    {
        get
        {
            int count = 0;

            foreach (CustomerWindow window in customerWindows)
            {
                if (window != null && window.HasOrder)
                {
                    count++;
                }
            }

            return count;
        }
    }

    private void Start()
    {
        InitializeWindows();

        if (gameManager == null ||
            gameManager.IsPlaying)
        {
            SpawnInitialOrders();
        }
    }

    private void InitializeWindows()
    {
        foreach (CustomerWindow window in customerWindows)
        {
            if (window != null)
            {
                window.Initialize(this);
            }
        }
    }

    private void SpawnInitialOrders()
    {
        foreach (CustomerWindow window in customerWindows)
        {
            SpawnOrder(window);
        }
    }

    private void SpawnOrder(CustomerWindow window)
    {
        if (window == null)
            return;

        if (gameManager != null &&
            !gameManager.IsPlaying)
        {
            return;
        }

        Order order =
            GenerateRandomOrder();

        window.SetOrder(order);
    }

    private Order GenerateRandomOrder()
    {
        int ingredientCount =
            Random.value < 0.5f
                ? 2
                : 3;

        List<IngredientData> ingredients =
            new List<IngredientData>();

        for (int i = 0;
             i < ingredientCount;
             i++)
        {
            int index =
                Random.Range(
                    0,
                    ingredientData.Length
                );

            ingredients.Add(
                ingredientData[index]
            );
        }

        return new Order(ingredients);
    }

    public void OrderCompleted(
        CustomerWindow window,
        int score)
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(score);
        }

        Debug.Log(
            $"Order completed for {score} points."
        );

        if (gameManager != null &&
            !gameManager.IsPlaying)
        {
            return;
        }

        StartCoroutine(
            RespawnOrderAfterDelay(window)
        );
    }

    private IEnumerator RespawnOrderAfterDelay(
        CustomerWindow window)
    {
        yield return new WaitForSeconds(
            respawnDelay
        );

        if (window == null)
            yield break;

        if (gameManager != null &&
            !gameManager.IsPlaying)
        {
            yield break;
        }

        SpawnOrder(window);
    }
}