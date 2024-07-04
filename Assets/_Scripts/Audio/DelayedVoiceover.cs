using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedVoiceover : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        StartCoroutine(PlayDelayedVoiceover());
    }

    private IEnumerator PlayDelayedVoiceover()
    {
        yield return new WaitForSeconds(_delay);
        _audioSource.Play();
    }
}
