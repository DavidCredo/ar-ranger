using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an ability that can be activated and deactivated.
/// </summary>
public interface IAbility
{
    /// <summary>
    /// Activates the ability.
    /// </summary>
    void Activate();

    /// <summary>
    /// Deactivates the ability.
    /// </summary>
    void Deactivate();
}
