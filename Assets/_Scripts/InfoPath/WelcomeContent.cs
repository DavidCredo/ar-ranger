using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the welcome content in the application.
/// </summary>
public class WelcomeContent : MonoBehaviour
{
    [SerializeField] private AudioClip _rangerVoiceover;
    [SerializeField] private GameObject[] _textToDeactivate;
    [SerializeField] private Texture2D _image;
    [SerializeField] private GameObject _framedImage;

    /// <summary>
    /// Gets the ranger voiceover audio clip.
    /// </summary>
    public AudioClip RangerVoiceover
    {
        get { return _rangerVoiceover; }
    }

    void OnEnable()
    {
        _framedImage.GetComponent<Renderer>().material.SetTexture("_BaseMap", _image);
        if (TeleportationPlacesStack.Instance.LastSceneName != string.Empty) changeContent();
    }

    /// <summary>
    /// Presents the ranger by playing the ranger voiceover and changing the content.
    /// </summary>
    public void PresentRanger()
    {
        GetComponentInChildren<SoundSourceManager>().PlayRangerVoiceover();
        changeContent();
    }

    private void changeContent()
    {
        foreach (var text in _textToDeactivate)
        {
            text.SetActive(false);
        }
        _framedImage.SetActive(true);
    }
}
