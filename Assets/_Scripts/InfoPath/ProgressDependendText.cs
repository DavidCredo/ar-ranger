using TMPro;
using UnityEngine;

public class ProgressiveText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCanvas;
    [SerializeField] private string _preScanText;
    [SerializeField] private string _postScanText;
    [SerializeField] private Creature _creature;

    private EventBinding<ScanEvent> _scanEvent;

    private void OnEnable()
    {
        _scanEvent = new EventBinding<ScanEvent>(OnScanEventRaised);
        EventBus<ScanEvent>.Register(_scanEvent);
    }

    private void OnDisable()
    {
        EventBus<ScanEvent>.Unregister(_scanEvent);
    }
    private void Start()
    {
        _textCanvas.text = _preScanText;
    }

    public void OnScanEventRaised(ScanEvent scan)
    {
        if (scan.IsComplete && scan.CreatureData.Name == _creature.Data.Name && _textCanvas.text != _postScanText)
        {
            _textCanvas.text = _postScanText;
        }
    }

}
