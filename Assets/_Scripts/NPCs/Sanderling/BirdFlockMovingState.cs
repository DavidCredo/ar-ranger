using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents the state where a bird flock is moving.
/// </summary>
public class BirdFlockMovingState : IState
{
    private BirdFlockController _birdFlockController;
    private GameStateMachine _birdFlockStateMachine;

    public BirdFlockMovingState(BirdFlockController birdFlockController, GameStateMachine birdFlockStateMachine)
    {
        _birdFlockController = birdFlockController;
        _birdFlockStateMachine = birdFlockStateMachine;
    }

    /// <summary>
    /// Called when the state is entered.
    /// Sets a random destination for the bird flock.
    /// </summary>
    public void Enter()
    {
        _birdFlockController.SetRandomDestination();
    }

    /// <summary>
    /// Called every frame while the state is active.
    /// Checks if each bird agent has reached its destination, and if so, changes the state to BirdFlockIdleState.
    /// </summary>
    public void Execute()
    {
        foreach (NavMeshAgent agent in _birdFlockController.BirdAgents)
        {
            if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 0.1f)
            {
                _birdFlockStateMachine.ChangeState(new BirdFlockIdleState(_birdFlockController, _birdFlockStateMachine));
            }
        }
    }

    /// <summary>
    /// Called when the state is exited.
    /// </summary>
    public void Exit()
    {
    }
}
