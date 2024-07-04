using UnityEngine;
using ARRanger.DependencyInjection;
using UnityEngine.Audio;
using DG.Tweening;

namespace ARRanger
{
    /// <summary>
    /// Manages the audio channels in the game, including volume transitions and low-pass cutoff transitions.
    /// Individual audio sources aren't managed here, but rather the audio mixer groups that they belong to.
    /// </summary>
    public class AudioManager : PersistentSingleton<AudioManager>, IDependencyProvider
    {
        [SerializeField] private AudioMixer _mainAudioMixer;
        [SerializeField] private string _masterVolumeParameter = "Master_Vol";
        [SerializeField] private string _ambianceVol = "Ambiance_Vol";
        [SerializeField] private string _sfxVolumeParameter = "SFX_Vol";
        [SerializeField] private string _dialogueVolumeParameter = "Dialogue_Vol";
        [SerializeField] private string _ambianceLoPassCutoffParameter = "Ambiance_LowPass_Freq";

        /// <summary>
        /// Represents the different audio channels that can be manipulated.
        /// </summary>
        public enum AudioChannel
        {
            Master,
            Ambiance,
            SFX,
            Dialogue
        }

        [Provide]
        AudioManager ProvideAudioManager()
        {
            return this;
        }

        void Start()
        {
            _mainAudioMixer.SetFloat(_ambianceVol, -80f);
        }

        /// <summary>
        /// Transitions the volume of a specific audio channel to a target volume over a specified duration.
        /// </summary>
        /// <param name="targetVolume">The target volume to transition to.</param>
        /// <param name="duration">The duration of the transition in seconds.</param>
        /// <param name="audioChannel">The audio channel to transition the volume of.</param>
        public void TransitionChannelVolume(float targetVolume, float duration, AudioChannel audioChannel)
        {
            if (targetVolume > 0)
            {
                targetVolume = 0;
            }
            else if (targetVolume < -80f)
            {
                targetVolume = -80f;
            }

            switch (audioChannel)
            {
                case AudioChannel.Master:
                    _mainAudioMixer.DOSetFloat(_masterVolumeParameter, targetVolume, duration).Play();
                    break;
                case AudioChannel.Ambiance:
                    _mainAudioMixer.DOSetFloat(_ambianceVol, targetVolume, duration).Play();
                    break;
                case AudioChannel.SFX:
                    _mainAudioMixer.DOSetFloat(_sfxVolumeParameter, targetVolume, duration).Play();
                    break;
                case AudioChannel.Dialogue:
                    _mainAudioMixer.DOSetFloat(_dialogueVolumeParameter, targetVolume, duration).Play();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Transitions the low-pass cutoff of the ambiance audio mixer.
        /// </summary>
        /// <param name="targetCutoff">The target cutoff value.</param>
        /// <param name="duration">The duration of the transition.</param>
        public void TransitionAmbianceLoPassCutoff(float targetCutoff, float duration)
        {
            _mainAudioMixer.DOSetFloat(_ambianceLoPassCutoffParameter, targetCutoff, duration).Play();
        }
    }
}
