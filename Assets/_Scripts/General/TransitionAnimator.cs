using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the fading animation of a mesh renderer.
/// </summary>
public class TransitionAnimator : MonoBehaviour
{
    [SerializeField] private float _fadeDuration;
    [SerializeField] private MeshRenderer _meshRenderer;
    private enum FadeMode { FadeIn, FadeOut }

    /// <summary>
    /// Initiates the fade-in animation.
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(Fade(FadeMode.FadeIn));
    }

    /// <summary>
    /// Initiates the fade-out animation.
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(Fade(FadeMode.FadeOut));
    }

    private IEnumerator Fade(FadeMode fadeMode)
    {
        float elapsedTime = 0f;
        while (elapsedTime<_fadeDuration)
        {
            if (fadeMode == FadeMode.FadeIn)
                _meshRenderer.material.color = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, elapsedTime / _fadeDuration));
            else if (fadeMode == FadeMode.FadeOut)
                _meshRenderer.material.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, elapsedTime / _fadeDuration));
            else
                Debug.LogError("Invalid fade mode");
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _meshRenderer.material.color = new Color(0f, 0f, 0f, 0f);
    }
}
