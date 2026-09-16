using UnityEngine;

public class PreparationUIManager : MonoBehaviour
{
    [Header("Progress Panels")]
    [SerializeField] private PreparationProgressUI chopProgress;
    [SerializeField] private PreparationProgressUI stoveProgress1;
    [SerializeField] private PreparationProgressUI stoveProgress2;

    private ChoppingStation choppingStation;
    private Stove stove;

    private void Start()
    {
        HideAll();
    }

    private void Update()
    {
        UpdateChoppingProgress();
        UpdateStoveProgress();
    }

    public void RegisterChoppingStation(
        ChoppingStation station)
    {
        choppingStation = station;
    }

    public void RegisterStove(
        Stove stoveStation)
    {
        stove = stoveStation;
    }

    private void UpdateChoppingProgress()
    {
        if (choppingStation == null)
        {
            chopProgress.Hide();
            return;
        }

        if (!choppingStation.IsChopping)
        {
            chopProgress.Hide();
            return;
        }

        chopProgress.Show();

        chopProgress.UpdateProgress(
            choppingStation.Progress,
            choppingStation.RemainingTime
        );
    }

    private void UpdateStoveProgress()
    {
        if (stove == null)
        {
            stoveProgress1.Hide();
            stoveProgress2.Hide();
            return;
        }

        UpdateStoveSlot(
            stove.Slot1,
            stoveProgress1
        );

        UpdateStoveSlot(
            stove.Slot2,
            stoveProgress2
        );
    }

    private void UpdateStoveSlot(
        CookingSlot slot,
        PreparationProgressUI progressUI)
    {
        if (slot == null || !slot.IsCooking)
        {
            progressUI.Hide();
            return;
        }

        progressUI.Show();

        progressUI.UpdateProgress(
            slot.Progress,
            slot.RemainingTime
        );
    }

    private void HideAll()
    {
        chopProgress.Hide();
        stoveProgress1.Hide();
        stoveProgress2.Hide();
    }
}