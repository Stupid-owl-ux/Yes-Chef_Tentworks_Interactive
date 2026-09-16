using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private IngredientData data;

    [Header("Visuals")]
    [SerializeField] private Renderer ingredientRenderer;
    [SerializeField] private Material rawMaterial;
    [SerializeField] private Material preparedMaterial;

    public IngredientData Data => data;
    public IngredientType Type => data.type;

    public IngredientState State { get; private set; }

    private void Awake()
    {
        State = IngredientState.Raw;
        UpdateVisual();
    }

    public void SetState(IngredientState newState)
    {
        State = newState;
        UpdateVisual();
    }

    public bool IsPrepared()
    {
        // Cheese is ready immediately.
        if (Type == IngredientType.Cheese)
            return true;

        return State == IngredientState.Chopped ||
               State == IngredientState.Cooked;
    }

    private void UpdateVisual()
    {
        if (ingredientRenderer == null)
            return;

        bool isPrepared =
            State == IngredientState.Chopped ||
            State == IngredientState.Cooked;

        ingredientRenderer.material =
            isPrepared
                ? preparedMaterial
                : rawMaterial;
    }
}