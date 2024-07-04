using DG.Tweening;
using UnityEngine;

/// <summary>
/// Controls the skipping of the onboarding process.
/// </summary>
public class SkipOnboardingController : MonoBehaviour
{
    [SerializeField] private GazeProgressController _gazeProgressController;

    [SerializeField] private OnboardingProgressController _onboardingProgressController;

    [SerializeField] private MRFeatureController _mrFeatureController;
    [SerializeField] private bool _skipOnboardingOnLoad = false;

    private OnboardingAbilitySystem _abilitySystem;

    private EventBinding<OnboardingStepCompleted> _onboardingStepCompletedBinding;

    private bool _wasSkipped = false;

    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().DOFade(1f, 5f).Play().SetEase(Ease.InExpo).From(0f);
    }

    void OnEnable()
    {
        _onboardingStepCompletedBinding = new EventBinding<OnboardingStepCompleted>(OnOnboardingStepCompleted);
        EventBus<OnboardingStepCompleted>.Register(_onboardingStepCompletedBinding);
        _abilitySystem = OnboardingAbilitySystem.Instance;
    }
    void OnDisable()
    {
        EventBus<OnboardingStepCompleted>.Unregister(_onboardingStepCompletedBinding);
    }

    void Update()
    {
        if (!_gazeProgressController.IsGazing)
        {
            _gazeProgressController.ResetGazeProgress();
        }
        if (_gazeProgressController.GazeProgress >= 1 && !_wasSkipped)
        {
            SkipOnboarding();
            _wasSkipped = true;
            gameObject.SetActive(false);
        }

    }

    private void SkipOnboarding()
    {
        Debug.Log("SkipOnboardingController.SkipOnboarding: Skipping onboarding");
        _abilitySystem.SkipOnboarding();
        _onboardingProgressController.SkipOnboarding();
        _mrFeatureController.DisablePassThrough();
    }


    private void OnOnboardingStepCompleted(OnboardingStepCompleted onboardingStepCompleted)
    {
        gameObject.SetActive(false);
    }
}
