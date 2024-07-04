using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a creature data scriptable object.
/// </summary>
[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]
public class CreatureDataSO : ScriptableObject, ICreature
{
    [field: SerializeField] public string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the creature has already been scanned.
    /// </summary>
    public bool AlreadyScanned { get; set; }

    /// <summary>
    /// Gets or sets the position where the creature was last scanned.
    /// </summary>
    public Vector3? ScannedAtPosition { get; set; }

    void OnEnable()
    {
        AlreadyScanned = false;
        ScannedAtPosition = null;
    }
}
