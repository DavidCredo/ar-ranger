
/// <summary>
/// Represents the idle state of the creature scanner.
/// </summary>
public class ScannerIdleState : IState
{
    private readonly CreatureScanner _scanner;

    public ScannerIdleState(CreatureScanner scanner)
    {
        _scanner = scanner;
    }

    /// <summary>
    /// Called when entering the idle state.
    /// </summary>
    public void Enter() { }

    /// <summary>
    /// Called every frame while in the idle state.
    /// </summary>
    public void Execute()
    {
        if (_scanner.IsUserAimingScanner())
        {
            _scanner.ScanForCreatures();
        }
        else
        {
            _scanner.StateMachine.ChangeState(new ScannerDisabledState(_scanner));
        }
    }

    /// <summary>
    /// Called when exiting the idle state.
    /// </summary>
    public void Exit()
    {
    }
}
