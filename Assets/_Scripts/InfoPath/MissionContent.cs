using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MissionContent : MonoBehaviour
{
    [SerializeField] private GameObject _textToDisable;
    [SerializeField] private Texture2D _pictureToFrame;
    [SerializeField] private CreatureDataSO _creatureData;
    [SerializeField] private AudioClip _rangerVoiceover; 

    private EventBinding<ScanEvent> _scanEventBinding;

    void OnEnable()
    {
        _scanEventBinding = new EventBinding<ScanEvent>(OnScanEvent);
        EventBus<ScanEvent>.Register(_scanEventBinding);

        gameObject.GetNamedChild("Framed Picture").GetComponent<Renderer>().material.SetTexture("_BaseMap", _pictureToFrame);

        if (_creatureData.AlreadyScanned)
        {
            HandleSuccess();
        }
    }

    void OnDisable()
    {
        EventBus<ScanEvent>.Unregister(_scanEventBinding);
    }

    private void OnScanEvent(ScanEvent scanEvent)
    {
        if (scanEvent.CreatureData.Name == _creatureData.Name &&
            scanEvent.CreatureData.AlreadyScanned)
        {
            HandleSuccess();
        }
    }

    private void HandleSuccess()
    {
        _textToDisable.SetActive(false);

        gameObject.GetNamedChild("Congratulations").GetComponent<TextMeshProUGUI>().text = "Super, Du hast etwas über eine " + _creatureData.Name + " gelernt!";
    }

    public void GetHelp()
    {
        GetComponentInChildren<SoundSourceManager>().PlayRangerVoiceover();
    }   

    public AudioClip RangerVoiceover
    {
        get { return _rangerVoiceover; }
    }
}
