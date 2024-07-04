using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a UI timer that displays a countdown.
/// </summary>
public class UITimer : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private GameObject _timeIndicator;

    private void OnEnable()
    {
        _timeIndicator.GetComponent<Image>().DOFillAmount(0, _time).Play();
    }
}
