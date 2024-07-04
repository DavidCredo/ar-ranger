using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Manages teleportation functionality in the game.
/// </summary>
public class TeleportatonManager : MonoBehaviour
{
    public void OnTeleportGesture()
    {
        EventBus<TeleportEvent>.Raise(new TeleportEvent());
    }
}
