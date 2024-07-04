using ARRanger;

/// <summary>
/// Represents an event that is triggered when an onboarding step is completed.
/// </summary>
public struct OnboardingStepCompleted : IEvent
{
    public OnboardingStepSO Step { get; private set; }
    public OnboardingStepCompleted(OnboardingStepSO step)
    {
        Step = step;
    }
}
