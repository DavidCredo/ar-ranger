using UnityEngine;

/// <summary>
/// Represents a trigger that detects when a player enters or leaves a prohibited area.
/// </summary>
public class ProhibitedAreaTrigger : MonoBehaviour
{
    [SerializeField] private bool _isMapBoarder = false;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _transparentMaterial;

    void Start()
    {
        _meshRenderer.material = _transparentMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerDirection = other.transform.position - transform.position;
            Vector3 prohibitedAreaOrientation = transform.up;
            float dotProduct = Vector3.Dot(playerDirection, prohibitedAreaOrientation);

            if (dotProduct > 0f)
            {
                // Player entered prohibited area
                EventBus<ProhibitedAreaEvent>.Raise(new ProhibitedAreaEvent { IsInProhibitedArea = true, ApproachingEndOfMap = _isMapBoarder });
            }
            else if (dotProduct < 0f)
            {
                // Player left prohibited area
                EventBus<ProhibitedAreaEvent>.Raise(new ProhibitedAreaEvent { IsInProhibitedArea = false, ApproachingEndOfMap = false });
            }
        }
    }
}
