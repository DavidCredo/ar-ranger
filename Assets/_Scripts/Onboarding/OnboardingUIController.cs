using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;
using System.Collections;

/// <summary>
/// Controls the UI elements and behavior for the onboarding process.
/// </summary>
public class OnboardingUIController : MonoBehaviour
{
    [SerializeField] private GazeProgressController _gazeProgressController;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _optionalInstructionsVideoPlane;
    [SerializeField] private Image _backgroundImage;
    private GameObject _transitionPanel;
    private GameObject _instructionsPanel;
    private TextMeshProUGUI _transitionText;
    private TextMeshProUGUI _instructionsText;

    private VideoPlayer _instructionVideoPlayer;
    private OnboardingStepSO _currentOnboardingStep;
    private EventBinding<PortalEnteredEvent> _onPortalEnteredEventBinding;

    public OnboardingUIController(bool isAnimating)
    {
        this.IsAnimating = isAnimating;

    }
    public bool IsAnimating { get; private set; } = false;

    void OnEnable()
    {
        _transitionPanel = transform.Find("TransitionPanel").gameObject;
        _instructionsPanel = transform.Find("InstructionsPanel").gameObject;

        _transitionText = _transitionPanel.transform.Find("TransitionText").GetComponent<TextMeshProUGUI>();

        _instructionsText = _instructionsPanel.transform.Find("InstructionText").GetComponent<TextMeshProUGUI>();
        _instructionVideoPlayer = _optionalInstructionsVideoPlane.GetComponent<VideoPlayer>();

        _onPortalEnteredEventBinding = new EventBinding<PortalEnteredEvent>(OnPortalEntered);
        EventBus<PortalEnteredEvent>.Register(_onPortalEnteredEventBinding);
    }

    void OnDisable()
    {
        EventBus<PortalEnteredEvent>.Unregister(_onPortalEnteredEventBinding);
    }

    void Start()
    {
        FadeInBackground(5f);
        AnimateUIIntoView(5f, 5f);
    }

    void Update()
    {
        if (_transitionPanel.activeSelf && _gazeProgressController.GazeProgress >= 1f)
        {
            OnGazeDwellTimeSatisfied();
        }
    }

    private void FadeInBackground(float duration)
    {

        _backgroundImage.DOFade(1f, duration).Play().SetEase(Ease.InExpo).From(0f);
    }

    /// <summary>
    /// Shows the transition panel with the specified transition text and executes the provided action when the gaze is complete.
    /// </summary>
    /// <param name="transitionText">The text to display in the transition panel.</param>
    /// <param name="onGazeComplete">The action to execute when the gaze is complete.</param>
    public void ShowTransitionPanel(OnboardingStepSO onboardingStep, string transitionText)
    {
        _gazeProgressController.ToggleGazeProgressIndicator(true);
        _currentOnboardingStep = onboardingStep;
        _gazeProgressController.ResetGazeProgress();
        _instructionsPanel.SetActive(false);
        _transitionPanel.SetActive(true);
        _transitionText.text = transitionText;
    }

    /// <summary>
    /// Shows the transition panel without using gaze interaction.
    /// </summary>
    /// <param name="onboardingStep">The onboarding step to display.</param>
    /// <param name="transitionText">The text to display in the transition panel.</param>
    public void ShowTransitionPanelNoGaze(OnboardingStepSO onboardingStep, string transitionText)
    {
        _gazeProgressController.ToggleGazeProgressIndicator(false);
        _currentOnboardingStep = onboardingStep;
        _instructionsPanel.SetActive(false);
        _transitionPanel.SetActive(true);
        _transitionText.text = transitionText;
    }

    /// <summary>
    /// Shows the instructions panel with the specified onboarding step.
    /// </summary>
    /// <param name="onboardingStep">The onboarding step to display.</param>
    public void ShowInstructionsPanel(AbilityOnboardingStepSO onboardingStep)
    {
        _currentOnboardingStep = onboardingStep;
        _gazeProgressController.ResetGazeProgress();
        _instructionsText.text = onboardingStep.StepDescription;
        if (onboardingStep.VideoClip != null)
        {
            _optionalInstructionsVideoPlane.SetActive(true);
            _instructionVideoPlayer.clip = onboardingStep.VideoClip;
            _transitionPanel.SetActive(false);
            _instructionsPanel.SetActive(true);
            _instructionVideoPlayer.Play();
        }
        else
        {
            _optionalInstructionsVideoPlane.SetActive(false);
        }
        _instructionsPanel.SetActive(true);
        _transitionPanel.SetActive(false);
    }

    /// <summary>
    /// Coroutine that completes the onboarding process.
    /// </summary>
    /// <returns>An IEnumerator representing the coroutine.</returns>
    public IEnumerator CompleteOnboarding()
    {
        _gazeProgressController.ToggleGazeProgressIndicator(false);
        _currentOnboardingStep = null;
        _instructionsPanel.SetActive(false);
        _transitionPanel.SetActive(true);
        _transitionText.richText = true;
        _transitionText.text = "Umgesetzt durch Jona König und David Credo <br> <br>WS23/24 FH-Kiel";
        yield return new WaitForSeconds(180f);
        gameObject.SetActive(false);
    }

    private void OnPortalEntered(PortalEnteredEvent @event)
    {
        AnimateUIIntoView(4f, 3f);
    }

    private void AnimateUIIntoView(float distanceToPlayer, float duration)
    {
        IsAnimating = true;
        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector3 targetPosition = _playerTransform.position + _playerTransform.forward * distanceToPlayer;

        rectTransform.DOMove(targetPosition, duration).SetEase(Ease.InOutSine).Play().OnComplete(() => IsAnimating = false);
        Quaternion targetRotation = Quaternion.LookRotation(_playerTransform.position - targetPosition);

        rectTransform.DORotateQuaternion(targetRotation, duration).SetEase(Ease.InOutSine).Play();

    }

    private void OnGazeDwellTimeSatisfied()
    {
        _gazeProgressController.ResetGazeProgress();
        EventBus<OnboardingStepCompleted>.Raise(new OnboardingStepCompleted(_currentOnboardingStep));
    }
}


