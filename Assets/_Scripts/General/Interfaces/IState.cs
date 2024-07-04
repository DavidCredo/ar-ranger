/// <summary>
/// Represents a state in a state machine.
/// </summary>
public interface IState
{
    /// <summary>
    /// Called when entering the state.
    /// </summary>
    void Enter();

    /// <summary>
    /// Called when executing the state.
    /// </summary>
    void Execute();

    /// <summary>
    /// Called when exiting the state.
    /// </summary>
    void Exit();
}