using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform slotPoint1;
    [SerializeField] private Transform slotPoint2;

    private CookingSlot slot1;
    private CookingSlot slot2;

    public CookingSlot Slot1 => slot1;
    public CookingSlot Slot2 => slot2;

    private void Awake()
    {
        slot1 = new CookingSlot();
        slot2 = new CookingSlot();
    }

    private void Start()
    {
        PreparationUIManager uiManager =
                FindAnyObjectByType<PreparationUIManager>();

        if (uiManager != null)
        {
            uiManager.RegisterStove(this);
        }
    }

    private void Update()
    {
        slot1.Update();
        slot2.Update();
    }

    public void Interact(PlayerInteractor interactor)
    {
        if (interactor == null)
            return;

        // Try to take finished meat first.
        if (interactor.Hand.IsEmpty)
        {
            if (TryTakeFinishedIngredient(
                slot1,
                interactor))
            {
                return;
            }

            if (TryTakeFinishedIngredient(
                slot2,
                interactor))
            {
                return;
            }
        }

        Ingredient heldIngredient =
            interactor.Hand.HeldIngredient;

        if (heldIngredient == null)
            return;

        if (heldIngredient.Type !=
            IngredientType.Meat)
        {
            Debug.Log(
                "Only meat can be cooked here."
            );

            return;
        }

        if (heldIngredient.State !=
            IngredientState.Raw)
        {
            Debug.Log(
                "This meat is already cooked."
            );

            return;
        }

        if (TryPlaceIngredient(
            slot1,
            slotPoint1,
            heldIngredient,
            interactor))
        {
            return;
        }

        if (TryPlaceIngredient(
            slot2,
            slotPoint2,
            heldIngredient,
            interactor))
        {
            return;
        }

        Debug.Log(
            "Both stove slots are occupied."
        );
    }

    private bool TryPlaceIngredient(
        CookingSlot slot,
        Transform slotPoint,
        Ingredient ingredient,
        PlayerInteractor interactor)
    {
        if (slot.IsOccupied)
            return false;

        interactor.Hand.RemoveIngredient();

        slot.PlaceIngredient(
            ingredient
        );

        ingredient.transform.SetParent(
            slotPoint
        );

        ingredient.transform.localPosition =
            Vector3.zero;

        ingredient.transform.localRotation =
            Quaternion.identity;

        Debug.Log(
            "Meat placed on stove."
        );

        return true;
    }

    private bool TryTakeFinishedIngredient(
        CookingSlot slot,
        PlayerInteractor interactor)
    {
        if (!slot.IsFinished)
            return false;

        Ingredient ingredient =
            slot.TakeIngredient();

        if (ingredient == null)
            return false;

        ingredient.transform.SetParent(null);

        bool pickedUp =
            interactor.Hand.TryPickup(
                ingredient
            );

        if (pickedUp)
        {
            Debug.Log(
                "Picked up cooked meat."
            );
        }

        return pickedUp;
    }
}