using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public List<IngredientData> RequiredIngredients { get; private set; }

    public float StartTime { get; private set; }

    public bool IsComplete =>
        RequiredIngredients.Count == 0;

    public float OpenDuration =>
        Time.time - StartTime;

    public int BaseScore { get; private set; }

    public Order(List<IngredientData> ingredients)
    {
        RequiredIngredients =
            new List<IngredientData>(ingredients);

        BaseScore = 0;

        foreach (IngredientData ingredient in ingredients)
        {
            if (ingredient != null)
            {
                BaseScore += ingredient.scoreValue;
            }
        }

        StartTime = Time.time;
    }

    public bool TryFulfill(Ingredient ingredient)
    {
        if (ingredient == null)
            return false;

        for (int i = 0;
             i < RequiredIngredients.Count;
             i++)
        {
            IngredientData required =
                RequiredIngredients[i];

            if (required.type != ingredient.Type)
                continue;

            if (!ingredient.IsPrepared())
                continue;

            RequiredIngredients.RemoveAt(i);

            return true;
        }

        return false;
    }

    public int CalculateScore()
    {
        int remainingTime =
            Mathf.FloorToInt(OpenDuration);

        return BaseScore - remainingTime;
    }
}