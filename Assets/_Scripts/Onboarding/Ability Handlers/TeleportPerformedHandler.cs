using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the teleport ability performed event and raises an onboarding step completed event.
/// </summary>
public class TeleportPerformedHandler : MonoBehaviour, IAbilityPerformedStrategy<TeleportEvent>
{
    [SerializeField] private OnboardingStepSO onboardingStep;

    private EventBinding<TeleportEvent> _eventBinding;

    private AbilityPerformedHandler<TeleportEvent> _abilityPerformedHandler;

    void OnEnable()
    {
        _eventBinding = new EventBinding<TeleportEvent>(Execute);
        _abilityPerformedHandler = new AbilityPerformedHandler<TeleportEvent>(_eventBinding);
        _abilityPerformedHandler.RegisterToEvent();
    }

    void OnDisable()
    {
        _abilityPerformedHandler.UnregisterFromEvent();
    }

    public void Execute(TeleportEvent abilityPerformedEvent)
    {
        EventBus<OnboardingStepCompleted>.Raise(new OnboardingStepCompleted(onboardingStep));
    }
}

