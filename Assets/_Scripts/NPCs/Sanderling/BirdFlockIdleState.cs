using UnityEngine;

/// <summary>
/// Represents the idle state of a bird flock in the game.
/// </summary>
public class BirdFlockIdleState : IState
{
    private BirdFlockController _birdFlockController;
    private GameStateMachine _birdFlockStateMachine;
    private float _idleStartTime;

    public BirdFlockIdleState(BirdFlockController birdFlockController, GameStateMachine birdFlockStateMachine)
    {
        _birdFlockController = birdFlockController;
        _birdFlockStateMachine = birdFlockStateMachine;
    }

    /// <summary>
    /// Called when the state is entered.
    /// </summary>
    public void Enter()
    {
        // Debug.Log("BirdFlockIdleState Enter");
        _birdFlockController.LetBirdsIdle();
        _idleStartTime = Time.time;
    }

    /// <summary>
    /// Called every frame while the state is active.
    /// </summary>
    public void Execute()
    {
        if (Time.time - _idleStartTime > _birdFlockController.IdleTime)
        {
            _birdFlockStateMachine.ChangeState(new BirdFlockMovingState(_birdFlockController, _birdFlockStateMachine));
        }
    }

    /// <summary>
    /// Called when the state is exited.
    /// </summary>
    public void Exit()
    {
        // Debug.Log("BirdFlockIdleState Exit");
    }
}
