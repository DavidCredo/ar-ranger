using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the sound sources and voiceovers of showcases.
/// </summary>
public class SoundSourceManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private AudioClip _whiteNoise;
    private float _volume;
    private AudioClip _RangerVoiceover;
    private AudioClip _ShowcaseVoiceover;
    private bool _isBusy = false;

    void OnEnable()
    {
        _whiteNoise = _audioSource.clip;
        _volume = _audioSource.volume;

        try
        {
            _RangerVoiceover = GetComponentInParent<InfoPathContent>().RangerVoiceover;
        }
        catch
        {
            try
            {
                _RangerVoiceover = GetComponentInParent<WelcomeContent>().RangerVoiceover;
            }
            catch
            {
                _RangerVoiceover = GetComponentInParent<MissionContent>().RangerVoiceover;
            }
        }

        _ShowcaseVoiceover = GetComponentInParent<InfoPathContent>().ShowcaseVoiceover;
    }

    public void PlayRangerVoiceover()
    {
        PlayVoiceover(_RangerVoiceover);
    }

    public void PlayShowcaseVoiceover()
    {
        PlayVoiceover(_ShowcaseVoiceover);
    }

    private void PlayVoiceover(AudioClip voiceover)
    {
        if (!_isBusy)
        {
            _isBusy = true;
            _audioSource.loop = false;
            _audioSource.Stop();
            _audioSource.volume = 1;
            _audioSource.clip = voiceover;
            _audioSource.Play();
            StartCoroutine(WaitForSound(voiceover));
        }
    }

    private IEnumerator WaitForSound(AudioClip audio)
    {
        yield return new WaitForSeconds(audio.length);

        resetToWhiteNoise();
        _isBusy = false;
    }

    private void resetToWhiteNoise()
    {
        _audioSource.volume = _volume;
        _audioSource.clip = _whiteNoise;
        _audioSource.Play();
        _audioSource.loop = true;
    }

}
