using UnityEngine;

public class TrashBin : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteractor interactor)
    {
        if (interactor == null)
            return;

        Ingredient heldIngredient =
            interactor.Hand.HeldIngredient;

        if (heldIngredient == null)
        {
            Debug.Log("Nothing to throw away.");
            return;
        }

        Ingredient discardedIngredient =
            interactor.Hand.RemoveIngredient();

        if (discardedIngredient != null)
        {
            Debug.Log(
                $"Discarded {discardedIngredient.Type}."
            );

            Destroy(
                discardedIngredient.gameObject
            );
        }
    }
}