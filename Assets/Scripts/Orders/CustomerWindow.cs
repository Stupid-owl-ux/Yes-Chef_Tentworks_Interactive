using UnityEngine;

public class CustomerWindow : MonoBehaviour, IInteractable
{
    [SerializeField] private OrderUI orderUI;
    [SerializeField] private FloatingScoreUI floatingScoreUI;

    private Order currentOrder;
    private OrderManager orderManager;

    public bool HasOrder =>
        currentOrder != null;

    public Order CurrentOrder =>
        currentOrder;

    public void Initialize(OrderManager manager)
    {
        orderManager = manager;
    }

    public void SetOrder(Order order)
    {
        currentOrder = order;

        if (orderUI != null)
        {
            orderUI.Show(currentOrder);
        }
    }

    public void ClearOrder()
    {
        currentOrder = null;

        if (orderUI != null)
        {
            orderUI.Hide();
        }
    }

    private void Update()
    {
        if (currentOrder == null)
            return;

        if (orderUI != null)
        {
            orderUI.UpdateOrder(currentOrder);
        }
    }

    public void Interact(PlayerInteractor interactor)
    {
        if (interactor == null)
            return;

        if (currentOrder == null)
        {
            Debug.Log(
                "No active order at this window."
            );

            return;
        }

        Ingredient heldIngredient =
            interactor.Hand.HeldIngredient;

        if (heldIngredient == null)
        {
            Debug.Log(
                "You need an ingredient in your hand."
            );

            return;
        }

        bool fulfilled =
            currentOrder.TryFulfill(
                heldIngredient
            );

        if (!fulfilled)
        {
            Debug.Log(
                "This ingredient is not required " +
                "or is not prepared."
            );

            return;
        }

        // Store the ingredient's individual value
        // before removing it from the player's hand.
        int ingredientScore =
            heldIngredient.Data.scoreValue;

        Ingredient delivered =
            interactor.Hand.RemoveIngredient();

        if (delivered != null)
        {
            Debug.Log(
                $"Delivered {delivered.Type} " +
                $"for +{ingredientScore}."
            );

            Destroy(
                delivered.gameObject
            );
        }

        // Show the individual ingredient score.
        if (floatingScoreUI != null)
        {
            floatingScoreUI.ShowScore(
                ingredientScore
            );
        }

        // Only calculate the order's final score
        // when every required ingredient has been delivered.
        if (currentOrder.IsComplete)
        {
            CompleteOrder();
        }
    }

    private void CompleteOrder()
    {
        int score =
            currentOrder.CalculateScore();

        Debug.Log(
            $"Order completed! Final score: {score}"
        );

        if (orderManager != null)
        {
            orderManager.OrderCompleted(
                this,
                score
            );
        }

        ClearOrder();
    }
}