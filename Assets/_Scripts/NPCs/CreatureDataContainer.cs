using System.Collections;
using System.Collections.Generic;
using ARRanger;
using ARRanger.DependencyInjection;
using UnityEngine;

/// <summary>
/// Singleton class that holds a collection of CreatureDataSO objects and provides methods to manage them.
/// </summary>
public class CreatureDataContainer : PersistentSingleton<CreatureDataContainer>
{
    [SerializeField] private List<CreatureDataSO> _creatures = new List<CreatureDataSO>();

    /// <summary>
    /// Gets the list of CreatureDataSO objects.
    /// </summary>
    public List<CreatureDataSO> Creatures => _creatures;

    private EventBinding<ScanEvent> _scanEventBinding;

    private void OnEnable()
    {
        _scanEventBinding = new EventBinding<ScanEvent>(OnScanEvent);
        EventBus<ScanEvent>.Register(_scanEventBinding);
    }

    private void OnDisable()
    {
        EventBus<ScanEvent>.Unregister(_scanEventBinding);
    }

    /// <summary>
    /// Checks if all creatures have been scanned.
    /// </summary>
    /// <returns>True if all creatures have been scanned, false otherwise.</returns>
    public bool WereAllCreaturesScanned()
    {
        foreach (var creature in _creatures)
        {
            if (!creature.AlreadyScanned)
            {
                return false;
            }
        }
        return true;
    }

    private void OnScanEvent(ScanEvent scanEvent)
    {
        if (WereAllCreaturesScanned())
        {
            EventBus<AllCreaturesScannedEvent>.Raise(new AllCreaturesScannedEvent());
        }
    }
}

public struct AllCreaturesScannedEvent : IEvent
{

}
