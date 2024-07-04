using ARRanger;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Base class for onboarding steps that can be used as ScriptableObjects.
/// </summary>
public abstract class OnboardingStepSO : ScriptableObject, IOnboardingStep
{
    public string StepName;
    public string StepDescription;
    public bool IsCompleted;
    public AudioClip Voiceover;

    void OnEnable()
    {
        IsCompleted = false;
    }

    public abstract void Initialize(OnboardingUIController onboardingUIController, OnboardingAudioController onboardingAudioController);
}
