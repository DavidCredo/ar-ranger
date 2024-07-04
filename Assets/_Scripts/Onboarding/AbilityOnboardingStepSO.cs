using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "OnboardingStep", menuName = "ARRanger/OnboardingSteps/Ability Step", order = 1)]
public class AbilityOnboardingStepSO : OnboardingStepSO
{
    public AbilitySO AbilitySO;
    public VideoClip VideoClip;
    public override void Initialize(OnboardingUIController onboardingUIController, OnboardingAudioController onboardingAudioController)
    {
        onboardingAudioController.InitializeOnboardingStep(this);
        onboardingUIController.ShowInstructionsPanel(this);
    }
}
