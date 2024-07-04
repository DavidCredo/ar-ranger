using System.Collections.Generic;
using ARRanger;
using UnityEngine;

/// <summary>
/// Represents a stack of teleportation places.
/// </summary>
public class TeleportationPlacesStack : PersistentSingleton<TeleportationPlacesStack>
{
    private Stack<TeleportationPlace> _teleportationPlaces = new Stack<TeleportationPlace>();

    public string LastSceneName { get; set; } = string.Empty;

    public TeleportationTrigger LastTeleportationTrigger { get; set; }

    public void AddTeleportationPlace(TeleportationPlace teleportationPlace)
    {
        _teleportationPlaces.Push(teleportationPlace);
    }

    public void RemoveLastTeleportationPlace()
    {
        _teleportationPlaces.Pop();
    }

    public TeleportationPlace GetLastTeleportationPlace()
    {
        return _teleportationPlaces.Peek();
    }

    public bool IsEmpty()
    {
        return !_teleportationPlaces.TryPeek(out _);
    }

    public void Clear()
    {
        _teleportationPlaces.Clear();
    }
}

/// <summary>
/// Represents a teleportation place with its position, rotation, and scene name.
/// </summary>
public struct TeleportationPlace
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public string SceneName { get; set; }
}
