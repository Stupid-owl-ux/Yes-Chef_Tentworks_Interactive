using UnityEngine;

public class ChoppingStation : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform ingredientPoint;

    private Ingredient currentIngredient;

    private float choppingFinishTime;
    private float choppingDuration;

    private bool isChopping;

    public bool IsOccupied => currentIngredient != null;

    public bool IsChopping => isChopping;

    public float RemainingTime { get; private set; }

    public float Progress
    {
        get
        {
            if (choppingDuration <= 0f)
                return 1f;

            return 1f -
                (RemainingTime / choppingDuration);
        }
    }

    private void Start()
    {
        PreparationUIManager uiManager =
            FindAnyObjectByType<PreparationUIManager>();

        if (uiManager != null)
        {
            uiManager.RegisterChoppingStation(this);
        }
    }

    private void Update()
    {
        if (!isChopping || currentIngredient == null)
            return;

        RemainingTime =
            Mathf.Max(
                0f,
                choppingFinishTime - Time.time
            );

        if (Time.time >= choppingFinishTime)
        {
            FinishChopping();
        }
    }

    public void Interact(PlayerInteractor interactor)
    {
        if (interactor == null)
            return;

        // Table already contains an ingredient.
        if (currentIngredient != null)
        {
            if (isChopping)
            {
                Debug.Log(
                    "Vegetable is still chopping."
                );

                return;
            }

            if (interactor.Hand.IsEmpty)
            {
                Ingredient ingredient =
                    currentIngredient;

                currentIngredient = null;

                interactor.Hand.TryPickup(
                    ingredient
                );

                Debug.Log(
                    "Picked up chopped vegetable."
                );
            }

            return;
        }

        // Table is empty.
        Ingredient heldIngredient =
            interactor.Hand.HeldIngredient;

        if (heldIngredient == null)
            return;

        if (heldIngredient.Type !=
            IngredientType.Vegetable)
        {
            Debug.Log(
                "Only vegetables can be chopped here."
            );

            return;
        }

        if (heldIngredient.State !=
            IngredientState.Raw)
        {
            Debug.Log(
                "This vegetable is already chopped."
            );

            return;
        }

        PlaceIngredient(
            heldIngredient,
            interactor
        );
    }

    private void PlaceIngredient(
        Ingredient ingredient,
        PlayerInteractor interactor)
    {
        interactor.Hand.RemoveIngredient();

        currentIngredient = ingredient;

        ingredient.transform.SetParent(
            ingredientPoint
        );

        ingredient.transform.localPosition =
            Vector3.zero;

        ingredient.transform.localRotation =
            Quaternion.identity;

        choppingDuration =
            ingredient.Data.preparationTime;

        choppingFinishTime =
            Time.time + choppingDuration;

        RemainingTime =
            choppingDuration;

        isChopping = true;

        Debug.Log(
            $"Chopping vegetable for " +
            $"{choppingDuration} seconds."
        );
    }

    private void FinishChopping()
    {
        isChopping = false;

        RemainingTime = 0f;

        currentIngredient.SetState(
            IngredientState.Chopped
        );

        Debug.Log(
            "Vegetable finished chopping."
        );
    }
}