using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the progression of the level based on the scanning of creatures.
/// </summary>
public class LevelProgressionController : MonoBehaviour
{
    [SerializeField] private GameObject _pathBlocker;
    [SerializeField] private GameObject _lightHouseSignal;

    private EventBinding<AllCreaturesScannedEvent> _allCreaturesScannedEventBinding;

    void OnEnable()
    {
        _allCreaturesScannedEventBinding = new EventBinding<AllCreaturesScannedEvent>(OnAllCreaturesScanned);
        EventBus<AllCreaturesScannedEvent>.Register(_allCreaturesScannedEventBinding);
        if (CreatureDataContainer.Instance.WereAllCreaturesScanned())
        {
            _pathBlocker.SetActive(false);
            _lightHouseSignal.SetActive(true);
        }
    }

    void OnDisable()
    {
        EventBus<AllCreaturesScannedEvent>.Unregister(_allCreaturesScannedEventBinding);
    }

    void OnAllCreaturesScanned(AllCreaturesScannedEvent allCreaturesScannedEvent)
    {
        _pathBlocker.SetActive(false);
        _lightHouseSignal.SetActive(true);
    }
}
