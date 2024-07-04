using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

/// <summary>
/// Controls the animation of the mussel and its associated objects, used for the showcase.
/// </summary>
public class MiesmuschelAnimation : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private List<GameObject> _byssusThreads;

    [SerializeField]
    private List<GameObject> _seastars;

    [SerializeField]
    private Transform _waterTransform;

    [SerializeField]
    private GameObject _mussel;

    [SerializeField]
    private GameObject _showoffBase;

    [SerializeField]
    private Transform _otherMusselsTransform;

    #endregion

    #region Local Variables
    private Transform _musselTransform;
    private Rigidbody _musselRigidbody;
    private List<Transform> _byssusThreadTransforms = new List<Transform>();
    private List<Transform> _seastarTransforms = new List<Transform>();
    private Sequence _sequence;
    int seastarIndex = 0;

    #endregion

    void OnEnable()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;

        _musselTransform = _mussel.transform;
        _musselRigidbody = _mussel.GetComponent<Rigidbody>();

        foreach (var thread in _byssusThreads)
        {
            _byssusThreadTransforms.Add(thread.transform);
        }

        foreach (var seastar in _seastars)
        {
            _seastarTransforms.Add(seastar.transform);
        }

        _sequence = DOTween.Sequence();
    }
    public void PlayAnimation()
    {
        // Water level rieses and mussel appears
        _sequence
        .Append(_waterTransform.DOLocalMoveY(0.8f, 3f))
        .Append(_musselTransform.DOLocalMoveY(0.61f, 1f))
        .AppendInterval(11f);

        // Mussel shoots out byssus threads
        foreach (var threadTransform in _byssusThreadTransforms) _sequence.Append(threadTransform.DOLocalMoveY(-1f, 0.5f));
        // foreach (var threadBody in _byssusThreads) _sequence.AppendCallback(() => threadBody.GetComponent<ConfigurableJoint>().connectedBody = _showoffBase.GetComponent<Rigidbody>());
        // _sequence.AppendInterval(2f);

        // // Mussel is moved by currents but holds onto the substrate
        // for (int i = 0; i < 3; i++)
        // {
        //     _sequence.Append(_musselRigidbody.DOMoveX(0.05f, 1f)).Rewind(false);
        //     _sequence.Append(_musselRigidbody.DOMoveX(-0.05f, 1f)).Rewind(false);
        // }
        // // Reset joints to allow for shrinking
        // foreach (var threadBody in _byssusThreads) _sequence.AppendCallback(() => threadBody.GetComponent<ConfigurableJoint>().connectedBody = null);

        // Mussels shrinks and other mussel appear
        _sequence
        .AppendInterval(5f)
        .Append(_musselTransform.DOScale(new Vector3(0.2f, 0.05662971f, 0.05131188f), 1f))
        .Append(_otherMusselsTransform.DOLocalMoveY(-8.853198f, 1f));

        // Seastars appear one by one
        foreach (var seastar in _seastarTransforms) _sequence.Append(seastar.DOLocalMoveY(0.18f, 0.5f));

        // Water level drops and seastars disappear one by one
        _sequence
        .Append(_waterTransform.DOLocalMoveY(0.05f, 3f))
        .AppendInterval(2f);
        foreach (var seastar in _seastars)
        {   // Normal for loops don't work with DOTween
            seastarIndex++;
            if (seastarIndex % 2 != 0)
            {
                _sequence
                .AppendCallback(() => seastar.SetActive(false))
                .AppendInterval(1f);
            }
        }
        _sequence.AppendInterval(2f);

        // Water level rises
        _sequence.Append(_waterTransform.DOLocalMoveY(0.8f, 3f));

        _sequence.Play();
    }
}
