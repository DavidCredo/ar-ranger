using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI for the scanner functionality.
/// </summary>
public class ScannerUIController : MonoBehaviour
{
    private const float INITIAL_FILL_AMOUNT = 0f;
    const float SCAN_COMPLETED_TIMER = 2f;
    const float SCAN_ABORTED_TIMER = 1f;
    [SerializeField] private TextMeshProUGUI _scanProgressLabel;
    [SerializeField] private Image _crosshair;
    private Color _defaultCrosshairColor;

    [SerializeField]
    [Tooltip("The slider that displays the scan progress.")]

    private Image _scanProgressBar;

    private EventBinding<ScanEvent> _scanEventBinding;
    private EventBinding<ProhibitedAreaEvent> _prohibitedAreaEventBinding;

    private void OnEnable()
    {
        _prohibitedAreaEventBinding = new EventBinding<ProhibitedAreaEvent>(OnProhibitedAreaEventRaised);
        _scanEventBinding = new EventBinding<ScanEvent>(OnScanEventRaised);
        EventBus<ProhibitedAreaEvent>.Register(_prohibitedAreaEventBinding);
        EventBus<ScanEvent>.Register(_scanEventBinding);
        _defaultCrosshairColor = _crosshair.color;
    }

    private void OnDisable()
    {
        EventBus<ProhibitedAreaEvent>.Unregister(_prohibitedAreaEventBinding);
        EventBus<ScanEvent>.Unregister(_scanEventBinding);
    }

    /// <summary>
    /// Updates the scan progress text with the provided scan progress value.
    /// </summary>
    /// <param name="scanProgress">The progress of the scan.</param>
    private void SetScanProgress(float scanProgress)
    {
        _scanProgressLabel.text = "Scanne...";
        _scanProgressBar.fillAmount = scanProgress;
    }

    private void SetScanStatusMessage(string message)
    {
        _scanProgressLabel.text = message;
    }

    private void ChangeCrosshairColor(Color color)
    {
        _crosshair.color = color;
    }

    /// <summary>
    /// Resets the scan progress UI after the specified number of seconds.
    /// </summary>
    /// <param name="seconds">The number of seconds to wait before resetting.</param>
    private IEnumerator ResetScanUIAfterSeconds(float seconds)
    {
        const string EMPTY_TEXT = "";

        _scanProgressBar.fillAmount = INITIAL_FILL_AMOUNT;
        yield return new WaitForSeconds(seconds);
        _scanProgressLabel.text = EMPTY_TEXT;
    }

    /// <summary>
    /// Handles the event raised when a scan is completed, running or aborted.
    /// </summary>
    /// <param name="scan">The scan object containing information about the scan.</param>
    public void OnScanEventRaised(ScanEvent scan)
    {
        if (scan.IsComplete)
        {
            SetScanStatusMessage("Scan abgeschlossen!");
            StartCoroutine(ResetScanUIAfterSeconds(SCAN_COMPLETED_TIMER));
        }
        else if (scan.IsAborting)
        {
            SetScanStatusMessage("Scan abgebrochen!");
            StartCoroutine(ResetScanUIAfterSeconds(SCAN_ABORTED_TIMER));
        }
        else
        {
            SetScanProgress(scan.ScanProgress);
        }
    }
    public void OnProhibitedAreaEventRaised(ProhibitedAreaEvent prohibitedAreaEvent)
    {
        return;
    }
}
