using UnityEngine;
using DG.Tweening;
using System.Collections;

/// <summary>
/// Represents an animation script for the Sanderling, to use in a showcase.
/// </summary>
public class SanderlingAnimation : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private Transform _birdTransform;

    [SerializeField]
    private GameObject _maleBird;

    [SerializeField]
    private GameObject _eggs;

    [SerializeField]
    private Transform _mapCamTransform;

    [SerializeField]
    private Transform _mapProjectionTransform;

    [SerializeField]
    private GameObject _groundCover;

    [SerializeField]
    private Material _icyMaterial;

    #endregion

    #region Local Variables
    private Sequence _firstSequence;
    private Sequence _secondSequence;
    private Transform _groundCoverTransform;

    #endregion

    void OnEnable()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;

        _firstSequence = DOTween.Sequence();
        _secondSequence = DOTween.Sequence();
        _groundCoverTransform = _groundCover.transform;
    }

    public void StartAnimation()
    {
        //  _firstSequence.AppendInterval(11f).WaitForCompletion(); does not work

        StartCoroutine(StartAnimationCoroutine());
    }

    private IEnumerator StartAnimationCoroutine()
    {
        yield return new WaitForSeconds(12f);

        // Bird rises into the air
        _firstSequence.Append(_birdTransform.DOLocalMove(new Vector3(0.39f, -2.5f, 4.7f), 11.0f))
        .Join(_groundCover.GetComponent<Renderer>().material.DOTiling(new Vector2(5.0f, 5.0f), 9.0f));

        // GroundCover and MapProjection switch places
        _firstSequence.Append(_groundCover.GetComponent<Renderer>().material.DOFade(0.0f, 5.0f))

        // MapCam zooms out (rises up)
        .Join(_mapCamTransform.DOLocalMoveY(-10f, 5.0f)).SetEase(Ease.OutBack);

        // MapCam flies towards Greenland
        _firstSequence.Append(_mapCamTransform.DOLocalMove(new Vector3(-14f, -10f, 33.73f), 12.0f))

        // MapCam zooms in
        .Append(_mapCamTransform.DOLocalMoveY(-17f, 10.0f)).SetEase(Ease.OutBack)

        // Material of GroundCover gets replaced by icy material
        .AppendCallback(() => _groundCover.GetComponent<Renderer>().material = _icyMaterial)

        // MapProjection and GroundCover switch places
        .Append(_groundCover.GetComponent<Renderer>().material.DOFade(1.0f, 2.0f))
        .Join(_groundCover.GetComponent<Renderer>().material.DOTiling(new Vector2(1.0f, 1.0f), 2.0f));

        // Bird lands
        _firstSequence.Append(_birdTransform.DOLocalMoveY(-3.24f, 5.0f));

        _firstSequence.Play();

        yield return new WaitForSeconds(21f);

        // Male appears, birds multiply ;)
        _secondSequence.AppendInterval(1.0f)
        .AppendCallback(() => _maleBird.SetActive(true))
        .AppendInterval(1.0f)
        .Append(_maleBird.transform.DOLocalJump(new Vector3(0.39f, -3.24f, 4.7f), 0.1f, 2, 1.0f))
        .AppendInterval(2.0f)
        .Append(_maleBird.transform.DOLocalJump(new Vector3(0.2f, -3.24f, 4.5f), 0.1f, 1, 1.0f))
        .AppendInterval(0.5f)
        .AppendCallback(() => _maleBird.SetActive(false))

        // generate four eggs underneith the bird
        .AppendCallback(() => _eggs.SetActive(true))

        // move bird aside
        .Append(_birdTransform.DOLocalJump(new Vector3(0.2f, -3.24f, 4.3f), 0.1f, 1, 1.0f));

        _secondSequence.Play();
    }
}
