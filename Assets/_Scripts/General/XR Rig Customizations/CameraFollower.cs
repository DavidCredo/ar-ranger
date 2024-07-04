using UnityEngine;

/// <summary>
/// Follows the position and rotation of the main camera.
/// </summary>
public class CameraFollower : MonoBehaviour
{
    private Transform _cameraTransform;

    void OnEnable()
    {
        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Quaternion cameraRotation = Quaternion.Euler(0f, _cameraTransform.rotation.eulerAngles.y, 0f);
        transform.SetPositionAndRotation(_cameraTransform.position, cameraRotation);
    }
}
