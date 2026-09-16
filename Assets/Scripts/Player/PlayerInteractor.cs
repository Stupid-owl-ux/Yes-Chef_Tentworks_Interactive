using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private Transform interactionOrigin;
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private GameManager gameManager;

    public PlayerHand Hand => playerHand;

    private void Update()
    {
        if (gameManager != null &&
            !gameManager.IsPlaying)
        {
            return;
        }

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Collider[] hits =
            Physics.OverlapSphere(
                interactionOrigin.position,
                interactionRange
            );

        IInteractable closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            IInteractable interactable =
                hit.GetComponent<IInteractable>();

            if (interactable == null)
                continue;

            float distance =
                Vector3.Distance(
                    interactionOrigin.position,
                    hit.transform.position
                );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }

        closestInteractable?.Interact(this);
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionOrigin == null)
            return;

        Gizmos.DrawWireSphere(
            interactionOrigin.position,
            interactionRange
        );
    }
}