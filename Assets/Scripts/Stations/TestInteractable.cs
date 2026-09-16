using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteractor interactor)
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }
}