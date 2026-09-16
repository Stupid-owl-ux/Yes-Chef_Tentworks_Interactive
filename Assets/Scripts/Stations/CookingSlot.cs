using UnityEngine;

public class CookingSlot
{
    public Ingredient Ingredient { get; private set; }

    public bool IsOccupied => Ingredient != null;

    public bool IsCooking { get; private set; }

    public bool IsFinished { get; private set; }

    public float RemainingTime { get; private set; }

    public float TotalTime { get; private set; }

    public float Progress
    {
        get
        {
            if (TotalTime <= 0f)
                return 1f;

            return 1f - (RemainingTime / TotalTime);
        }
    }

    private float finishTime;

    public void PlaceIngredient(Ingredient ingredient)
    {
        Ingredient = ingredient;

        IsCooking = true;
        IsFinished = false;

        TotalTime = ingredient.Data.preparationTime;

        finishTime = Time.time + TotalTime;

        RemainingTime = TotalTime;
    }

    public void Update()
    {
        if (!IsCooking || Ingredient == null)
            return;

        RemainingTime =
            Mathf.Max(
                0f,
                finishTime - Time.time
            );

        if (Time.time >= finishTime)
        {
            FinishCooking();
        }
    }

    private void FinishCooking()
    {
        IsCooking = false;
        IsFinished = true;
        RemainingTime = 0f;

        Ingredient.SetState(
            IngredientState.Cooked
        );
    }

    public Ingredient TakeIngredient()
    {
        if (!IsFinished)
            return null;

        Ingredient ingredient = Ingredient;

        Ingredient = null;

        IsFinished = false;
        IsCooking = false;

        RemainingTime = 0f;
        TotalTime = 0f;

        return ingredient;
    }
}