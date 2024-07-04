using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SchulpAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _water;
    [SerializeField]
    private GameObject _seaFloor;
    [SerializeField]
    private GameObject _sepiaGameObject;
    [SerializeField]
    private Material _angryMaterial;
    private GameObject _sepiaBody;
    private GameObject _sepiaHead;
    private Sequence _animationSequence;

    private Material _seaFloorMaterial;


    void OnEnable()
    {
        _sepiaBody = _sepiaGameObject.transform.Find("Body").gameObject;
        _sepiaHead = _sepiaGameObject.transform.Find("Head").gameObject;
        _seaFloorMaterial = _seaFloor.GetComponent<MeshRenderer>().material;
        DOTween.Init();
    }

    public void StartAnimation()
    {
        Debug.Log("StartAnimation");
        _animationSequence = DOTween.Sequence();

        _animationSequence.AppendInterval(10f);
        _animationSequence.Append(_water.transform.DOLocalMoveY(3.2f, 1f).SetEase(Ease.Linear));

        _animationSequence.Insert(0, _sepiaGameObject.transform.DOLocalMoveY(3f, 1f).SetEase(Ease.Linear));
        _animationSequence.AppendInterval(1f);
        _animationSequence.AppendCallback(() =>
        {
            _sepiaBody.SetActive(true);
            _sepiaHead.SetActive(true);
        });
        _animationSequence.AppendInterval(2f);
        _animationSequence.Append(_sepiaBody.GetComponent<MeshRenderer>().material.DOFade(1, 1f).SetEase(Ease.Linear));
        _animationSequence.AppendInterval(9f);
        _animationSequence.Append(_sepiaGameObject.transform.DOLocalMoveY(2.8f, 1f).SetEase(Ease.Linear));
        _animationSequence.Append(_seaFloor.transform.DOLocalMoveY(2.979f, 1f).SetEase(Ease.Linear));
        _animationSequence.AppendInterval(3f);
        _animationSequence.AppendCallback(() => ActivateSepiaColorAdaptation());
        _animationSequence.AppendInterval(10f);
        _animationSequence.AppendCallback(() => ActivateAngrySepia());
        _animationSequence.Play();
    }

    void ActivateSepiaColorAdaptation()
    {
        var meshRenderers = _sepiaGameObject.GetComponentsInChildren<MeshRenderer>();

        if (meshRenderers == null)
        {
            return;
        }

        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.material.DOColor(_seaFloorMaterial.color, 1f);
        }
    }

    void ActivateAngrySepia()
    {
        var meshRenderers = _sepiaGameObject.GetComponentsInChildren<MeshRenderer>();

        if (meshRenderers == null)
        {
            return;
        }

        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.material.DOColor(_angryMaterial.color, 1f);
        }
    }

}
