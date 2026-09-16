using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Transform handPoint;

    public Ingredient HeldIngredient { get; private set; }

    public bool IsEmpty => HeldIngredient == null;

    public bool TryPickup(Ingredient ingredient)
    {
        if (ingredient == null || !IsEmpty)
            return false;

        HeldIngredient = ingredient;

        ingredient.transform.SetParent(handPoint);
        ingredient.transform.localPosition = Vector3.zero;
        ingredient.transform.localRotation = Quaternion.identity;

        SetIngredientCollider(ingredient, false);

        return true;
    }

    public Ingredient RemoveIngredient()
    {
        Ingredient ingredient = HeldIngredient;

        if (ingredient == null)
            return null;

        HeldIngredient = null;

        ingredient.transform.SetParent(null);

        SetIngredientCollider(ingredient, true);

        return ingredient;
    }

    private void SetIngredientCollider(
        Ingredient ingredient,
        bool enabled)
    {
        Collider[] colliders =
            ingredient.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = enabled;
        }
    }
}