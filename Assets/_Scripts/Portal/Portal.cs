using UnityEngine;

/// <summary>
/// Represents a portal in the game.
/// </summary>
public class Portal : MonoBehaviour
{
    private MRFeatureController _mrFeatureController;

    void OnEnable()
    {
        _mrFeatureController = FindObjectOfType<MRFeatureController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _mrFeatureController.DisablePassThrough();
            EventBus<PortalEnteredEvent>.Raise(new PortalEnteredEvent());
            Debug.Log("Portal Entered!");
            gameObject.SetActive(false);
        }
    }
}
