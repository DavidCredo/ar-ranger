using ARRanger.DependencyInjection;
using UnityEngine;

/// <summary>
/// Represents the ability to scan for creatures.
/// </summary>
public class ScanAbility : MonoBehaviour
{
    public AbilitySO ScanAbilitySO;
    [Inject, SerializeField] private CreatureScanner _creatureScanner;

    void OnEnable()
    {
        ScanAbilitySO.onActivate += OnActivate;
        ScanAbilitySO.onDeactivate += OnDeactivate;
    }

    void OnDisable()
    {
        ScanAbilitySO.onActivate -= OnActivate;
        ScanAbilitySO.onDeactivate -= OnDeactivate;
    }

    public void OnActivate()
    {
        Debug.Log("Activating scan ability");
        _creatureScanner.transform.parent.gameObject.SetActive(true);
    }

    public void OnDeactivate()
    {
        Debug.Log("Deactivating scan ability");
        _creatureScanner.transform.parent.gameObject.SetActive(false);
    }
}
