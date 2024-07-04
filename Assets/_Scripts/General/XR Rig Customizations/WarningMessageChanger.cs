using System.Collections;
using TMPro;
using UnityEngine;

public class WarningMessageChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _warningMessage;
    private EventBinding<ProhibitedAreaEvent> _prohibitedAreaEventBinding;


    void OnEnable()
    {
        _prohibitedAreaEventBinding = new EventBinding<ProhibitedAreaEvent>(OnProhibitedAreaEvent);
        EventBus<ProhibitedAreaEvent>.Register(_prohibitedAreaEventBinding);
        _warningMessage.text = "Ranger ruft: HEY! Bitte nicht ins Naturschutzgebiet!";
        StartCoroutine(ChangeMessageSeverityAfterSeconds("Ranger ruft: Ernsthaft, wir müssen die Natur schützen! Komm zurück!", 10f));
    }

    void OnDisable()
    {
        EventBus<ProhibitedAreaEvent>.Unregister(_prohibitedAreaEventBinding);
    }

    private IEnumerator ChangeMessageSeverityAfterSeconds(string message, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _warningMessage.text = message;
    }

    /* TODO: Den Text in Abhängigkeit davon anzuzeigen, ob man sich dem Ende der Karte nähert, 
    ist nicht Aufgabe dieser Klasse (sondern von WaringMessageEventListener). 
    Später wird es aber eh keinen Text, sondern Ton geben, daher wird diese komplette Klasse 
    später verworfen und der Umsatnd ist akzeptabel.
    */
    private void OnProhibitedAreaEvent(ProhibitedAreaEvent prohibitedAreaEvent)
    {
        if (prohibitedAreaEvent.ApproachingEndOfMap)
        {
            StartCoroutine(ChangeMessageSeverityAfterSeconds("Ranger ruft: So geht das nicht, ich hole dich zurück.", 17f));
        }
    }

}
