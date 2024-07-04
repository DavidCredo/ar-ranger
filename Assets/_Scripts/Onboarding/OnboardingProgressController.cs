using System.Collections;
using System.Collections.Generic;
using ARRanger.DependencyInjection;
using UnityEngine;

/// <summary>
/// Manages the progress of the onboarding process and controls the UI flow.
/// </summary>
public class OnboardingProgressController : MonoBehaviour
{
    [SerializeField] private List<OnboardingStepSO> _onboardingSteps;

    [Inject, SerializeField] private IAbilitySystem _abilitySystem;

    [SerializeField] private AudioSource _uiAudioSource;

    [SerializeField] private OnboardingAudioController _onboardingAudioController;

    private EventBinding<OnboardingStepCompleted> _onboardingStepCompletedBinding;
    private OnboardingUIController _onboardingUIController;

    private int _currentStepIndex = 0;

    void Awake()
    {
        if (TeleportationPlacesStack.Instance.LastSceneName != string.Empty) gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _onboardingUIController = GetComponent<OnboardingUIController>();
        _onboardingStepCompletedBinding = new EventBinding<OnboardingStepCompleted>(OnOnboardingStepCompleted);
        EventBus<OnboardingStepCompleted>.Register(_onboardingStepCompletedBinding);
    }

    IEnumerator Start()
    {
        while (_onboardingUIController.IsAnimating)
        {
            yield return null;
        }
        if (_currentStepIndex >= _onboardingSteps.Count)
        {
            yield break;
        }
        _onboardingSteps[_currentStepIndex].Initialize(_onboardingUIController, _onboardingAudioController);
    }

    void OnDisable()
    {
        EventBus<OnboardingStepCompleted>.Unregister(_onboardingStepCompletedBinding);
    }

    /// <summary>
    /// Event handler for when an onboarding step is completed.
    /// </summary>
    /// <param name="onboardingStepCompleted">The completed onboarding step.</param>
    private void OnOnboardingStepCompleted(OnboardingStepCompleted onboardingStepCompleted)
    {
        if (_currentStepIndex >= _onboardingSteps.Count)
        {
            return;
        }

        if (onboardingStepCompleted.Step == _onboardingSteps[_currentStepIndex] && !onboardingStepCompleted.Step.IsCompleted)
        {
            Debug.Log($"Onboarding step completed: {onboardingStepCompleted.Step.StepName}");
            if (onboardingStepCompleted.Step is AbilityOnboardingStepSO)
            {
                _uiAudioSource.Play();
            }

            _onboardingSteps[_currentStepIndex].IsCompleted = true;
            ProceedToNextStep();
        }
        else if (onboardingStepCompleted.Step == null)
        {
            Debug.LogError("Onboarding step is null");
        }
    }

    private void ProceedToNextStep()
    {
        _currentStepIndex++;
        if (_currentStepIndex < _onboardingSteps.Count)
        {
            if (_onboardingSteps[_currentStepIndex] is AbilityOnboardingStepSO abilityOnboardingStep)
            {
                _abilitySystem.ActivateAbility(abilityOnboardingStep.AbilitySO);
            }

            _onboardingSteps[_currentStepIndex].Initialize(_onboardingUIController, _onboardingAudioController);
        }
        else if (_currentStepIndex >= _onboardingSteps.Count)
        {
            StartCoroutine(_onboardingUIController.CompleteOnboarding());
            Debug.Log("Onboarding complete");
        }
    }

    public void SkipOnboarding()
    {
        _currentStepIndex = _onboardingSteps.Count;
        StartCoroutine(_onboardingUIController.CompleteOnboarding());
    }
}
