using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the execution of a scan event and performs necessary actions based on the scan progress.
/// </summary>
public class ScanPerformedHandler : MonoBehaviour, IAbilityPerformedStrategy<ScanEvent>
{
    [SerializeField] private OnboardingStepSO onboardingStep;

    [SerializeField] private GameObject _exampleCube;
    private EventBinding<ScanEvent> _eventBinding;
    private AbilityPerformedHandler<ScanEvent> _abilityPerformedHandler;

    void OnEnable()
    {
        _eventBinding = new EventBinding<ScanEvent>(Execute);
        _abilityPerformedHandler = new AbilityPerformedHandler<ScanEvent>(_eventBinding);
        _abilityPerformedHandler.RegisterToEvent();
    }

    void OnDisable()
    {
        _abilityPerformedHandler.UnregisterFromEvent();
    }

    /// <summary>
    /// Executes the necessary actions based on the scan event.
    /// If the scan progress is greater than or equal to 0.9, raises an event indicating the completion of the onboarding step and destroys the example cube.
    /// </summary>
    /// <param name="scanEvent">The scan event containing the scan progress.</param>
    public void Execute(ScanEvent scanEvent)
    {
        if (scanEvent.ScanProgress >= 0.9f)
        {
            EventBus<OnboardingStepCompleted>.Raise(new OnboardingStepCompleted(onboardingStep));

            Destroy(_exampleCube);
        }
    }
}
