using UnityEngine;
using UnityEngine.InputSystem;

public class Refrigerator : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient vegetablePrefab;
    [SerializeField] private Ingredient cheesePrefab;
    [SerializeField] private Ingredient meatPrefab;

    private PlayerInteractor currentInteractor;
    private bool selectionOpen;

    public void Interact(PlayerInteractor interactor)
    {
        if (!interactor.Hand.IsEmpty)
        {
            Debug.Log("Hands are full.");
            return;
        }

        if (selectionOpen)
            return;

        currentInteractor = interactor;
        selectionOpen = true;

        Debug.Log("Select ingredient: [1] Vegetable  [2] Cheese  [3] Meat");
    }

    private void Update()
    {
        if (!selectionOpen)
            return;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SpawnIngredient(vegetablePrefab);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SpawnIngredient(cheesePrefab);
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            SpawnIngredient(meatPrefab);
        }

        // Escape cancels selection.
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseSelection();
        }
    }

    private void SpawnIngredient(Ingredient prefab)
    {
        if (prefab == null || currentInteractor == null)
            return;

        if (!currentInteractor.Hand.IsEmpty)
        {
            CloseSelection();
            return;
        }

        Ingredient ingredient = Instantiate(prefab);

        bool pickedUp = currentInteractor.Hand.TryPickup(ingredient);

        if (!pickedUp)
        {
            Destroy(ingredient.gameObject);
            return;
        }

        Debug.Log($"Picked up {ingredient.Type}");

        CloseSelection();
    }

    private void CloseSelection()
    {
        selectionOpen = false;
        currentInteractor = null;
    }
}