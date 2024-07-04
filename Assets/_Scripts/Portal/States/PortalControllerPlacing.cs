using UnityEngine;

public class PortalControllerPlacing : IState
{

    private PortalController _portalController;

    private Vector3 _placementPosition;

    private Vector3 _placementNormal;

    public PortalControllerPlacing(PortalController portalController, Vector3 placementPosition, Vector3 placementNormal)
    {
        _portalController = portalController;
        _placementPosition = placementPosition;
        _placementNormal = placementNormal;
    }

    public void Enter()
    {
        _portalController.PlacePortal(_placementPosition, _placementNormal);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
