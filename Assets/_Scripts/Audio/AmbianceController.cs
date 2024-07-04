using ARRanger;
using ARRanger.DependencyInjection;
using UnityEngine;

/// <summary>
/// Controls the ambiance audio in the game.
/// </summary>
public class AmbianceController : PersistentSingleton<AmbianceController>
{
    [SerializeField, Inject] private AudioManager _audioManager;

    private EventBinding<SceneTransitionEvent> _sceneTransitionEventBinding;
    private EventBinding<PortalEnteredEvent> _portalEnteredCallback;

    [SerializeField] private float _transitionDuration = 0.4f;

    void OnEnable()
    {
        _sceneTransitionEventBinding = new EventBinding<SceneTransitionEvent>(TransitionToMuffled);
        _portalEnteredCallback = new EventBinding<PortalEnteredEvent>(OnPortalEntered);
        EventBus<PortalEnteredEvent>.Register(_portalEnteredCallback);
        EventBus<SceneTransitionEvent>.Register(_sceneTransitionEventBinding);
    }

    void OnDisable()
    {
        EventBus<SceneTransitionEvent>.Unregister(_sceneTransitionEventBinding);
        EventBus<PortalEnteredEvent>.Unregister(_portalEnteredCallback);
    }

    /// <summary>
    /// Transitions the audio to a muffled state by setting the low pass cutoff to 2000 Hz and reducing the volume by 10 dB.
    /// </summary>
    public void TransitionToMuffled()
    {
        _audioManager.TransitionAmbianceLoPassCutoff(2000f, _transitionDuration);
        _audioManager.TransitionChannelVolume(-10f, 0.4f, AudioManager.AudioChannel.Ambiance);
    }

    /// <summary>
    /// Transitions the audio to a normal state by setting the low pass cutoff to 20000 Hz and increasing the volume by 10 dB.
    /// </summary>
    public void TransitionToNormal()
    {
        _audioManager.TransitionAmbianceLoPassCutoff(20000f, _transitionDuration);
        _audioManager.TransitionChannelVolume(0f, 0.4f, AudioManager.AudioChannel.Ambiance);
    }

    private void OnPortalEntered(PortalEnteredEvent portalEnteredEvent)
    {
        Debug.Log("Changing ambiance volume.");
        _audioManager.TransitionAmbianceLoPassCutoff(20000f, 2f);
        _audioManager.TransitionChannelVolume(0f, 2f, AudioManager.AudioChannel.Ambiance);
    }
}
