using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

/// <summary>
/// Represents the content of an information path.
/// </summary>
public class InfoPathContent : MonoBehaviour {

    #region SerializeFields

    [SerializeField]
    private string _trivialName;

    [SerializeField]
    private string _scientificName;

    [SerializeField]
    private string _size;

    [SerializeField]
    private string _weight;

    [SerializeField]
    private string _age;

    [SerializeField]
    private string _habitat;

    [SerializeField]
    private string _food;

    [SerializeField]
    private string _congratulations;

    [SerializeField]
    private string _funFact;

    [SerializeField]
    private Texture2D _image;

    // [SerializeField]
    // private GameObject _ShowcasePrefab;

    [SerializeField]
    private AudioClip _rangerVoiceover;

    [SerializeField]
    private AudioClip _showcaseVoiceover;

    #endregion

    #region private fields

    public AudioClip RangerVoiceover
    {
        get { return _rangerVoiceover; }
    }

    public AudioClip ShowcaseVoiceover
    {
        get { return _showcaseVoiceover; }
    }

    #endregion

    void Start() {
        // switch empty showcase with dedicated one -> fixed for now to pair showcase button with infopath speaker
        // var showOffCoordinates = gameObject.GetNamedChild("Show Off").transform.position;
        // gameObject.GetNamedChild("Show Off").SetActive(false);
        // Instantiate(_ShowcasePrefab, gameObject.transform);
        // _ShowcasePrefab.transform.position = showOffCoordinates;

        // set content
        gameObject.GetNamedChild("Congratulations").GetComponent<TextMeshProUGUI>().text = _congratulations;
        gameObject.GetNamedChild("FunfactContent").GetComponent<TextMeshProUGUI>().text = _funFact;
        gameObject.GetNamedChild("Framed Picture").GetComponent<Renderer>().material.SetTexture("_BaseMap", _image);
        gameObject.GetNamedChild("TrivialName").GetComponent<TextMeshProUGUI>().text = _trivialName;
        gameObject.GetNamedChild("ScientificName").GetComponent<TextMeshProUGUI>().text = _scientificName;
        gameObject.GetNamedChild("Size").GetComponent<TextMeshProUGUI>().text = "Größe: " + _size;
        gameObject.GetNamedChild("Weight").GetComponent<TextMeshProUGUI>().text = "Gewicht: " + _weight;
        gameObject.GetNamedChild("Age").GetComponent<TextMeshProUGUI>().text = "Alter: " + _age;
        gameObject.GetNamedChild("Habitat").GetComponent<TextMeshProUGUI>().text = "Lebensraum: " + _habitat;
        gameObject.GetNamedChild("Food").GetComponent<TextMeshProUGUI>().text = "Nahrung: " + _food;
    }
}