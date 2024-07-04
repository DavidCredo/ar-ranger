using UnityEngine;
using UnityEngine.XR.Hands;

/// <summary>
/// Controls the visibility of hand meshes in a virtual reality environment.
/// </summary>
public class HandVisualController : MonoBehaviour
{
    [SerializeField] private XRHandMeshController _leftHandMesh;

    [SerializeField] private XRHandMeshController _rightHandMesh;

    private EventBinding<PortalEnteredEvent> eventBinding;

    void OnEnable()
    {
        eventBinding = new EventBinding<PortalEnteredEvent>(OnPortalEntered);
        EventBus<PortalEnteredEvent>.Register(eventBinding);
    }

    void Start()
    {
        ToggleHandMeshVisibility(false);
    }

    void OnDisable()
    {
        EventBus<PortalEnteredEvent>.Unregister(eventBinding);
    }

    private void ToggleHandMeshVisibility(bool makeVisible)
    {
        _leftHandMesh.showMeshWhenTrackingIsAcquired = makeVisible;
        _rightHandMesh.showMeshWhenTrackingIsAcquired = makeVisible;
    }

    private void OnPortalEntered(PortalEnteredEvent portalEntered)
    {
        ToggleHandMeshVisibility(true);
    }
}
