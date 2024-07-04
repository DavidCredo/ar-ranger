using UnityEngine;

/// <summary>
/// Represents an onboarding step that is triggered by an external event.
/// </summary>
[CreateAssetMenu(fileName = "OnboardingStep", menuName = "ARRanger/OnboardingSteps/External Event Step", order = 1)]
public class ExternalEventOnboardingStepSO : OnboardingStepSO
{
    public EventType EventType;

    void OnEnable()
    {
        IsCompleted = false;

        switch (EventType)
        {
            case EventType.PortalEntered:
                EventBinding<PortalEnteredEvent> portalEventBinding = new EventBinding<PortalEnteredEvent>(OnEventRaised);
                EventBus<PortalEnteredEvent>.Register(portalEventBinding);
                break;
            default:
                break;
        }
    }

    public override void Initialize(OnboardingUIController onboardingUIController, OnboardingAudioController onboardingAudioController)
    {
        onboardingAudioController.InitializeOnboardingStep(this);
        onboardingUIController.ShowTransitionPanelNoGaze(this, StepDescription);
    }

    public void OnEventRaised()
    {
        Debug.Log("External Event raised");
        EventBus<OnboardingStepCompleted>.Raise(new OnboardingStepCompleted(this));
    }
}

[System.Serializable]
public enum EventType
{
    PortalEntered,
}
