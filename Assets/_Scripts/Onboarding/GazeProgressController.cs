using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the gaze progress functionality for an object in the scene.
/// </summary>
public class GazeProgressController : MonoBehaviour
{
    private bool _isGazing;
    [SerializeField] private bool _triggerGazeEvents = true;
    [SerializeField] private Image _gazeProgressIndicator;
    public float GazeProgress => _gazeProgressIndicator.fillAmount;

    public bool IsGazing
    {
        get { return _isGazing; }
        set
        {
            if (_isGazing != value && _triggerGazeEvents)
            {
                _isGazing = value;
                EventBus<GazeStatusChangedEvent>.Raise(new GazeStatusChangedEvent(_isGazing));
            }
            else
            {
                _isGazing = value;
            }
        }
    }

    [field: SerializeField] public float RequiredGazeTime { get; set; } = 4f;

    void Update()
    {
        if (IsGazing && _gazeProgressIndicator.isActiveAndEnabled)
        {
            UpdateGazeProgressIndicator();
        }
    }

    public void SetIsGazing(bool isGazing)
    {
        if (_gazeProgressIndicator.isActiveAndEnabled)
        {
            IsGazing = isGazing;
        }
    }

    public void UpdateGazeProgressIndicator()
    {
        if (_gazeProgressIndicator.fillAmount <= 1f)
        {
            _gazeProgressIndicator.fillAmount += Time.deltaTime / RequiredGazeTime;
        }
    }

    public void ResetGazeProgress()
    {
        _gazeProgressIndicator.fillAmount = 0f;
    }

    public void ToggleGazeProgressIndicator(bool isEnabled)
    {
        _gazeProgressIndicator.gameObject.SetActive(isEnabled);
    }
}


public struct GazeStatusChangedEvent : IEvent
{
    public bool IsGazing;

    public GazeStatusChangedEvent(bool isGazing)
    {
        IsGazing = isGazing;
    }
}
