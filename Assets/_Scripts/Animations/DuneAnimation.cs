using UnityEngine;
using DG.Tweening;

/// <summary>
/// Controls the animation for the Salzmiere Showcase.
/// </summary>
public class DuneAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform _salzMierenTransform;
    [SerializeField]
    private Transform _duneTransform;
    [SerializeField]
    private ParticleSystem _sandParticleSystem;

    private Sequence _animationSequence;
    void OnEnable()
    {
        DOTween.Init();
    }
    public void StartAnimation()
    {
        _animationSequence = DOTween.Sequence();

        _animationSequence
        .AppendInterval(18f)
        .Append(_salzMierenTransform.DOLocalMoveY(0.5f, 1f).SetEase(Ease.Linear))
        .AppendInterval(1f)
        .AppendCallback(() => _sandParticleSystem.Play())
        .AppendInterval(1f)
        .Append(_duneTransform.DOLocalMoveY(0.045f, 1f).SetEase(Ease.Linear))
        .AppendCallback(() => _sandParticleSystem.Stop())
        .Append(_salzMierenTransform.DOLocalMoveY(0.6f, 1f).SetEase(Ease.Linear))
        .AppendInterval(1f)
        .AppendCallback(() => _sandParticleSystem.Play())
        .AppendInterval(1f)
        .Append(_duneTransform.DOLocalMoveY(0.1f, 1f).SetEase(Ease.Linear))
        .AppendCallback(() => _sandParticleSystem.Stop())
        .Append(_salzMierenTransform.DOLocalMoveY(0.75f, 1f).SetEase(Ease.Linear))
        .AppendInterval(1f)
        .AppendCallback(() => _sandParticleSystem.Play())
        .AppendInterval(1f)
        .Append(_duneTransform.DOLocalMoveY(0.12f, 1f).SetEase(Ease.Linear))
        .AppendCallback(() => _sandParticleSystem.Stop());

        _animationSequence.Play();
    }
}
