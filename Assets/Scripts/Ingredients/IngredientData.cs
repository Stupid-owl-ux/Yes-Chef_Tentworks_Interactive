using UnityEngine;

[CreateAssetMenu(
    fileName = "IngredientData",
    menuName = "Yes Chef/Ingredient Data"
)]
public class IngredientData : ScriptableObject
{
    [Header("Identity")]
    public IngredientType type;

    [Header("Scoring")]
    public int scoreValue;

    [Header("Preparation")]
    public bool requiresPreparation;
    public float preparationTime;
}