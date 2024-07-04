using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a text-only step in the onboarding process.
/// </summary>
[CreateAssetMenu(fileName = "OnboardingStep", menuName = "ARRanger/OnboardingSteps/Text-only Step", order = 1)]
public class TextOnboardingStepSO : OnboardingStepSO
{
    /// <summary>
    /// Initializes the text-only onboarding step.
    /// </summary>
    /// <param name="onboardingUIController">The UI controller for the onboarding process.</param>
    /// <param name="onboardingAudioController">The audio controller for the onboarding process.</param>
    public override void Initialize(OnboardingUIController onboardingUIController, OnboardingAudioController onboardingAudioController)
    {
        onboardingAudioController.InitializeOnboardingStep(this);
        onboardingUIController.ShowTransitionPanel(this, StepDescription);
    }
}
