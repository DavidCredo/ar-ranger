using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This script ensures that the teleportation area is enabled when at least one creature has been scanned.
/// </summary>
public class EnsureTeleportEnabled : MonoBehaviour
{
    [SerializeField] private TeleportationArea _teleportationArea;

    /// <summary>
    /// Called when the script is enabled.
    /// Checks if any creature has been scanned and enables the teleportation area if true.
    /// </summary>
    void OnEnable()
    {
        CreatureDataContainer creatureDataContainer = CreatureDataContainer.Instance;

        foreach (var creature in creatureDataContainer.Creatures)
        {
            if (creature.AlreadyScanned)
            {
                _teleportationArea.enabled = true;
                break;
            }
        }
    }
}
