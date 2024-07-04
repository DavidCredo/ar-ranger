using UnityEngine;

/// <summary>
/// Represents a creature in the game.
/// </summary>
public interface ICreature
{
    /// <summary>
    /// Gets the name of the creature.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the creature has already been scanned.
    /// </summary>
    bool AlreadyScanned { get; set; }

    /// <summary>
    /// Gets or sets the position where the creature was last scanned.
    /// </summary>
    public Vector3? ScannedAtPosition { get; set; }
}
