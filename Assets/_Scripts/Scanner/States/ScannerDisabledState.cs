using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the state of the scanner when it is disabled.
/// </summary>
public class ScannerDisabledState : IState
{
    private CreatureScanner _scanner;

    public ScannerDisabledState(CreatureScanner scanner)
    {
        _scanner = scanner;
    }

    /// <summary>
    /// Called when entering the ScannerDisabledState.
    /// Disables the laser of the scanner.
    /// </summary>
    public void Enter()
    {
        _scanner.DisableLaser();
    }

    /// <summary>
    /// Called repeatedly while in the ScannerDisabledState.
    /// Checks if the user is aiming the scanner and changes the state to ScannerIdleState if true.
    /// </summary>
    public void Execute()
    {
        if (_scanner.IsUserAimingScanner())
        {
            _scanner.StateMachine.ChangeState(new ScannerIdleState(_scanner));
        }
    }

    /// <summary>
    /// Called when exiting the ScannerDisabledState.
    /// Enables the laser of the scanner.
    /// </summary>
    public void Exit()
    {
        _scanner.EnableLaser();
    }
}

