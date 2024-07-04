using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the audio playback and gaze interaction for onboarding steps.
/// </summary>
public class OnboardingAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GazeProgressController _gazeProgressController;

    private EventBinding<GazeStatusChangedEvent> _gazeStatusChangedEventBinding;

    void OnEnable()
    {
        _gazeStatusChangedEventBinding = new EventBinding<GazeStatusChangedEvent>(OnGazeStatusChanged);
        EventBus<GazeStatusChangedEvent>.Register(_gazeStatusChangedEventBinding);
    }

    void OnDisable()
    {
        EventBus<GazeStatusChangedEvent>.Unregister(_gazeStatusChangedEventBinding);
    }

    public void InitializeOnboardingStep(OnboardingStepSO onboardingStep)
    {
        if (onboardingStep.Voiceover != null)
        {
            _audioSource.clip = onboardingStep.Voiceover;
            _gazeProgressController.RequiredGazeTime = onboardingStep.Voiceover.length;
            Debug.Log($"OnboardingAudioController.InitializeOnboardingStep: RequiredGazeTime: {_gazeProgressController.RequiredGazeTime}");
            PlayVoiceOver();
            if (onboardingStep.StepName == "Scanner Introduction")
            {
                StartCoroutine(CheckPlaybackCompletion(onboardingStep));
            }
        }
    }
    private IEnumerator CheckPlaybackCompletion(OnboardingStepSO step)
    {
        yield return new WaitForSeconds(_audioSource.clip.length - _audioSource.time);
        EventBus<OnboardingStepCompleted>.Raise(new OnboardingStepCompleted(step));
    }
    private void PlayVoiceOver()
    {
        if (_audioSource.clip != null)
        {
            _audioSource.Play();
        }
    }

    private void OnGazeStatusChanged(GazeStatusChangedEvent gazeStatusChangedEvent)
    {
        if (_audioSource.clip == null)
        {
            return;
        }

        if (gazeStatusChangedEvent.IsGazing)
        {
            _audioSource.UnPause();
        }
        else if (!gazeStatusChangedEvent.IsGazing)
        {
            _audioSource.Pause();
        }
    }

}
