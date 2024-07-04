using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Controls the passthrough functionality of the XR rig.
/// </summary>
public class PassthroughToggle : MonoBehaviour
{
    [SerializeField] private ARCameraManager _aRCameraManager;

    private EventBinding<PortalEnteredEvent> _portalEnteredEventBinding;

    void OnEnable()
    {
        _portalEnteredEventBinding = new EventBinding<PortalEnteredEvent>(OnPortalEntered);
        EventBus<PortalEnteredEvent>.Register(_portalEnteredEventBinding);
    }

    void OnDisable()
    {
        EventBus<PortalEnteredEvent>.Unregister(_portalEnteredEventBinding);
    }

    void OnPortalEntered(PortalEnteredEvent portalEnteredEvent)
    {
        _aRCameraManager.enabled = false;
        Camera.main.clearFlags = CameraClearFlags.Skybox;
    }
}
