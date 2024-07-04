using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;

/// <summary>
/// Detects a teleport gesture based on the hand's palm normal and the global up vector.
/// </summary>
public class DetectTeleportGesture : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _gestureDetectionThreshold = 0.8f;

    public float GestureDetectionThreshold { get => _gestureDetectionThreshold; private set => _gestureDetectionThreshold = value; }

    [SerializeField]
    private UnityEvent _onTeleportGestureDetected;

    [SerializeField]
    private UnityEvent _onTeleportGestureCancelled;
    void Start()
    {
        XRHandSubsystem m_Subsystem =
       XRGeneralSettings.Instance?
           .Manager?
           .activeLoader?
           .GetLoadedSubsystem<XRHandSubsystem>();
        if (m_Subsystem == null)
        {
            Debug.LogError("No hand subsystem available. Please ensure there is at least one hand tracker present in the XR Plugin Management settings.");
            return;
        }

        m_Subsystem.updatedHands += OnHandUpdate;
    }

    /// <summary>
    /// Callback method called when the hand tracking subsystem updates.
    /// Calculates the dot product of the hand's palm normal and the global up vector.
    /// If the dot product is greater than the
    /// </summary>
    /// <param name="subsystem">The XRHandSubsystem that triggered the update.</param>
    /// <param name="updateSuccessFlags">Flags indicating the success of the update.</param>
    /// <param name="updateType">The type of update being performed.</param>
    void OnHandUpdate(XRHandSubsystem subsystem,
                  XRHandSubsystem.UpdateSuccessFlags updateSuccessFlags,
                  XRHandSubsystem.UpdateType updateType)
    {
        Vector3 globalUp = Vector3.up;
        float dotProduct;


        var trackingData = subsystem.leftHand.GetJoint(XRHandJointID.Palm);


        if (updateType == XRHandSubsystem.UpdateType.BeforeRender)
        {
            if (trackingData.TryGetPose(out Pose pose))
            {
                dotProduct = Vector3.Dot(-pose.up.normalized, globalUp);

                if (dotProduct > GestureDetectionThreshold)
                {
                    _onTeleportGestureDetected.Invoke();
                }
                else
                {
                    _onTeleportGestureCancelled.Invoke();
                }

            }

        }
    }
}
