using UnityEngine;

/// <summary>
/// Handles the placement of a portal in the onboarding process.
/// </summary>
public class PortalPlacedHandler : MonoBehaviour, IAbilityPerformedStrategy<PortalPlacedEvent>
{
    [SerializeField] private OnboardingStepSO _onboardingStepSO;
    private EventBinding<PortalPlacedEvent> _eventBinding;
    private AbilityPerformedHandler<PortalPlacedEvent> _abilityPerformedHandler;

    /// <summary>
    /// Executes the logic when a portal is placed.
    /// </summary>
    /// <param name="abilityPerformedEvent">The event containing information about the placed portal.</param>
    public void Execute(PortalPlacedEvent abilityPerformedEvent)
    {
        EventBus<OnboardingStepCompleted>.Raise(new OnboardingStepCompleted(_onboardingStepSO));
    }

    void OnEnable()
    {
        _eventBinding = new EventBinding<PortalPlacedEvent>(Execute);
        _abilityPerformedHandler = new AbilityPerformedHandler<PortalPlacedEvent>(_eventBinding);
        _abilityPerformedHandler.RegisterToEvent();
    }

    void OnDisable()
    {
        _abilityPerformedHandler.UnregisterFromEvent();
    }
}
