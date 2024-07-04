using UnityEngine;


/// <summary>
/// Represents a state machine that manages the execution of different states.
/// </summary>
public class GameStateMachine
{
    private IState _currentState;


    /// <param name="startingState">The initial state of the state machine.</param>
    public GameStateMachine(IState startingState)
    {
        CurrentState = startingState;
        CurrentState?.Enter();
    }

    public IState CurrentState { get => _currentState; private set => _currentState = value; }

    /// <summary>
    /// Changes the current state of the state machine.
    /// </summary>
    /// <param name="newState">The new state to set.</param>
    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
        // Debug.Log($"Changed state to {CurrentState}");
    }

    /// <summary>
    /// Updates the current state of the state machine.
    /// </summary>
    public void Update()
    {
        CurrentState?.Execute();
    }
}
