using ARRanger;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Controls the behavior of a portal in the game.
/// </summary>
public class PortalController : MonoBehaviour
{
    [SerializeField] private GameObject _portalPrefab;
    [SerializeField] private GameObject _placementVisual;
    [SerializeField] private XRRayInteractor _rayInteractor;
    [SerializeField] private Transform _playerTransform;
    private EventBinding<PortalEnteredEvent> portalEnteredCallback;

    private GameObject _activePortal = null;


    private GameStateMachine _stateMachine;

    private Camera _camera;

    public Vector3 PlacementPosition { get; private set; }

    public Vector3 PlacementNormal { get; private set; }

    void OnEnable()
    {
        _stateMachine = new GameStateMachine(new PortalControllerIdle(this));
        portalEnteredCallback = new EventBinding<PortalEnteredEvent>(OnPortalEntered);
        EventBus<PortalEnteredEvent>.Register(portalEnteredCallback);
        _camera = Camera.main;
    }

    void OnDisable()
    {
        _stateMachine = null;
        EventBus<PortalEnteredEvent>.Unregister(portalEnteredCallback);
    }

    void Update()
    {
        _stateMachine.Update();
    }

    public void CheckForRaycastHit()
    {
        if (!_rayInteractor.isActiveAndEnabled)
        {
            return;
        }

        if (_rayInteractor.TryGetCurrent3DRaycastHit(out var res))
        {
            if (Vector3.Distance(res.point, _camera.transform.position) > 10f)
            {
                _placementVisual.SetActive(false);
                return;
            }
            PlacementPosition = res.point;
            PlacementNormal = res.normal;
            ShowPlacementVisualAt(PlacementPosition, PlacementNormal);
        }
        else
        {
            _placementVisual.SetActive(false);
        }
    }

    public void OnGesturePerformed()
    {
        if (_placementVisual.activeSelf)
        {
            _stateMachine.ChangeState(new PortalControllerPlacing(this, PlacementPosition, PlacementNormal));
        }
    }

    void ShowPlacementVisualAt(Vector3 position, Vector3 hitNormal)
    {
        _placementVisual.SetActive(true);
        _placementVisual.transform.SetPositionAndRotation(position, Quaternion.LookRotation(hitNormal));
    }

    public void PlacePortal(Vector3 placementPosition, Vector3 placementNormal)
    {
        if (_activePortal != null)
        {
            Destroy(_activePortal);
        }
        Renderer portalRenderer = _portalPrefab.GetComponentInChildren<Renderer>();
        float portalHeight = portalRenderer.bounds.size.y;
        Vector3 portalPlacementPosWithOffset = placementPosition.With(y: placementPosition.y + portalHeight / 2f);

        Quaternion targetRotation = Quaternion.LookRotation(_playerTransform.position - portalPlacementPosWithOffset);

        GameObject portal = Instantiate(_portalPrefab, portalPlacementPosWithOffset, targetRotation);
        portal.transform.Rotate(-90f, -180f, 0f);
        portal.SetActive(true);
        _placementVisual.SetActive(false);
        _activePortal = portal;
        EventBus<PortalPlacedEvent>.Raise(new PortalPlacedEvent());
        _stateMachine.ChangeState(new PortalControllerIdle(this));
    }

    private void OnPortalEntered(PortalEnteredEvent portalEnteredEvent)
    {
        _placementVisual.SetActive(false);
        gameObject.SetActive(false);
    }

}
