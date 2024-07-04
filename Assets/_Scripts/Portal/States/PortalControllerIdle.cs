namespace ARRanger
{

    public class PortalControllerIdle : IState
    {
        private PortalController _portalController;

        public PortalControllerIdle(PortalController portalController)
        {
            _portalController = portalController;
        }

        public void Enter()
        {

        }

        public void Execute()
        {

            _portalController.CheckForRaycastHit();
        }

        public void Exit()
        {

        }
    }
}
