using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the event when a scan is aborted.
/// </summary>
public class ScanAbortedHandler : MonoBehaviour, IAbilityPerformedStrategy<ScanEvent>
{
    [SerializeField] private OnboardingStepSO onboardingStep;
    [SerializeField] private CreatureDataSO _exampleCubeData;
    private EventBinding<ScanEvent> _eventBinding;
    private AbilityPerformedHandler<ScanEvent> _abilityPerformedHandler;

    void OnEnable()
    {
        _exampleCubeData.AlreadyScanned = false;
        _eventBinding = new EventBinding<ScanEvent>(Execute);
        _abilityPerformedHandler = new AbilityPerformedHandler<ScanEvent>(_eventBinding);
        _abilityPerformedHandler.RegisterToEvent();
    }

    void OnDisable()
    {
        _abilityPerformedHandler.UnregisterFromEvent();
    }

    /// <summary>
    /// Executes the logic when a scan event is received.
    /// </summary>
    /// <param name="scanEvent">The scan event.</param>
    public void Execute(ScanEvent scanEvent)
    {
        if (scanEvent.IsAborting)
        {
            EventBus<OnboardingStepCompleted>.Raise(new OnboardingStepCompleted(onboardingStep));
        }
    }
}
