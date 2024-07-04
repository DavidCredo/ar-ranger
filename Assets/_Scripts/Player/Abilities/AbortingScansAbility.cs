using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an ability that allows the player to abort ongoing scans.
/// </summary>
public class AbortingScansAbility : MonoBehaviour
{
    public AbilitySO AbortingScansAbilitySO;

    void OnEnable()
    {
        AbortingScansAbilitySO.onActivate += OnActivate;
        AbortingScansAbilitySO.onDeactivate += OnDeactivate;
    }

    private void OnDeactivate()
    {
        Debug.Log("Scan Ability needs to be deactivated, to prohibit aborting scans.");
    }

    private void OnActivate()
    {
        Debug.Log("Scan Ability just needs to be activated to be able to abort scans.");
    }

    void OnDisable()
    {
        AbortingScansAbilitySO.onActivate -= OnActivate;
        AbortingScansAbilitySO.onDeactivate -= OnDeactivate;
    }
}
