using System.Text;
using TMPro;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ingredientsText;
    [SerializeField] private TMP_Text timerText;

    public void Show(Order order)
    {
        gameObject.SetActive(true);

        UpdateOrder(order);
    }

    public void UpdateOrder(Order order)
    {
        if (order == null)
            return;

        StringBuilder builder =
            new StringBuilder();

        foreach (IngredientData ingredient
                 in order.RequiredIngredients)
        {
            builder.Append(GetIngredientSymbol(
                ingredient.type));

            builder.Append(" ");

            builder.Append(
                ingredient.type
            );

            builder.Append("\n");
        }

        ingredientsText.text =
            builder.ToString();

        timerText.text =
            FormatTime(order.OpenDuration);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private string FormatTime(float seconds)
    {
        int totalSeconds =
            Mathf.FloorToInt(seconds);

        int minutes =
            totalSeconds / 60;

        int remainingSeconds =
            totalSeconds % 60;

        return $"{minutes:00}:{remainingSeconds:00}";
    }

    private string GetIngredientSymbol(
        IngredientType type)
    {
        switch (type)
        {
            case IngredientType.Vegetable:
                return "[V]";

            case IngredientType.Cheese:
                return "[C]";

            case IngredientType.Meat:
                return "[M]";

            default:
                return "[?]";
        }
    }
}