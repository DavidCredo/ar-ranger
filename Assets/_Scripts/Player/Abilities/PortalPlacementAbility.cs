using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents an ability that allows the player to place portals and teleport to designated areas.
/// </summary>
public class PortalPlacementAbility : MonoBehaviour
{
    public AbilitySO PortalPlacementAbilitySO;
    [SerializeField] private PortalController _portalController;
    [SerializeField] private TeleportationArea _teleportationArea;
    private EventBinding<PortalEnteredEvent> _eventBinding;

    void OnEnable()
    {
        _eventBinding = new EventBinding<PortalEnteredEvent>(OnPortalEntered);
        EventBus<PortalEnteredEvent>.Register(_eventBinding);

        PortalPlacementAbilitySO.onActivate += OnActivate;
        PortalPlacementAbilitySO.onDeactivate += OnDeactivate;
    }

    void OnDisable()
    {
        EventBus<PortalEnteredEvent>.Unregister(_eventBinding);
        PortalPlacementAbilitySO.onActivate -= OnActivate;
        PortalPlacementAbilitySO.onDeactivate -= OnDeactivate;
    }

    public void OnActivate()
    {
        _portalController.enabled = true;
        _teleportationArea.enabled = true;
    }

    public void OnDeactivate()
    {
        _portalController.enabled = false;
    }

    private void OnPortalEntered(PortalEnteredEvent portalEnteredEvent)
    {
        OnDeactivate();
    }
}
