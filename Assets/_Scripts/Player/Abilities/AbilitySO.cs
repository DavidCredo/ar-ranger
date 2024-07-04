using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a ScriptableObject that defines an ability in the game.
/// </summary>
[CreateAssetMenu(fileName = "Ability", menuName = "ARRanger/Ability", order = 1)]
public class AbilitySO : ScriptableObject, IAbility
{
    public Action onActivate;
    public Action onDeactivate;

    /// <summary>
    /// Activates the ability by invoking the onActivate event.
    /// </summary>
    public void Activate()
    {
        onActivate?.Invoke();
    }

    /// <summary>
    /// Deactivates the ability by invoking the onDeactivate event.
    /// </summary>
    public void Deactivate()
    {
        onDeactivate?.Invoke();
    }
}
