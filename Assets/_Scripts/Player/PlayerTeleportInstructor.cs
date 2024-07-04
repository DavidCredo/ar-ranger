using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the teleportation of the player character and performs further actions based on the teleportation trigger.
/// </summary>
public class PlayerTeleportInstructor : MonoBehaviour
{
    [SerializeField] private TeleportationManager _teleportationManager;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _viewManipulator;
    [SerializeField] private GameObject _uiTimer;

    /// <summary>
    /// Called when the script is enabled. Places the player character at the last teleportation location.
    /// </summary>
    void OnEnable()
    {
        PlaceMe();
    }

    private void PlaceMe()
    {
        if (TeleportationPlacesStack.Instance.IsEmpty()) return;

        TeleportationPlace teleportationPlace = TeleportationPlacesStack.Instance.GetLastTeleportationPlace();
        TeleportationPlacesStack.Instance.RemoveLastTeleportationPlace();

        _player.transform.position = teleportationPlace.Position;
        _player.transform.rotation = teleportationPlace.Rotation;

        CheckFurtherActions();
    }

    private void CheckFurtherActions()
    {
        if (TeleportationPlacesStack.Instance.LastTeleportationTrigger == TeleportationTrigger.Quiz)
        {
            StartCoroutine(TeleportBack());
            _viewManipulator.SetActive(true);
            _uiTimer.SetActive(true);
        }
    }

    private IEnumerator TeleportBack()
    {
        yield return new WaitForSeconds(30f);
        _teleportationManager.ReturnToLastPlace();
    }
}
