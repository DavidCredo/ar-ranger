using UnityEngine;

/// <summary>
/// Listens for the ProhibitedAreaEvent and updates the visibility of a warning message changer GameObject accordingly.
/// </summary>
public class WarningMessageEventListener : MonoBehaviour
{
    [SerializeField] private GameObject _warningMessageChanger;

    private EventBinding<ProhibitedAreaEvent> _prohibitedAreaEvent;

    void OnEnable()
    {
        _prohibitedAreaEvent = new EventBinding<ProhibitedAreaEvent>(OnEventRaised);
        EventBus<ProhibitedAreaEvent>.Register(_prohibitedAreaEvent);
    }

    void OnDisable()
    {
        EventBus<ProhibitedAreaEvent>.Unregister(_prohibitedAreaEvent);
    }

    /// <summary>
    /// Handles the ProhibitedAreaEvent and updates the visibility of the warning message changer GameObject.
    /// </summary>
    /// <param name="prohibitedAreaEvent">The ProhibitedAreaEvent containing information about whether the user is in a prohibited area.</param>
    public void OnEventRaised(ProhibitedAreaEvent prohibitedAreaEvent)
    {
        if (prohibitedAreaEvent.IsInProhibitedArea)
        {
            _warningMessageChanger.SetActive(true);
        }
        else
        {
            _warningMessageChanger.SetActive(false);
        }
    }
}
