using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateExampleCube : MonoBehaviour
{
    [SerializeField] private GameObject _exampleCube;
    [SerializeField] private OnboardingStepSO _scannerIntroStep;
    private EventBinding<OnboardingStepCompleted> _onboardingStepCompletedBinding;

    void OnEnable()
    {
        _onboardingStepCompletedBinding = new EventBinding<OnboardingStepCompleted>(OnScannerIntroStepCompleted);
        EventBus<OnboardingStepCompleted>.Register(_onboardingStepCompletedBinding);
    }

    void OnDisable()
    {
        EventBus<OnboardingStepCompleted>.Unregister(_onboardingStepCompletedBinding);
    }

    private void OnScannerIntroStepCompleted(OnboardingStepCompleted onboardingStepCompleted)
    {
        if (onboardingStepCompleted.Step == _scannerIntroStep)
        {
            _exampleCube.SetActive(true);
        }
    }
}
