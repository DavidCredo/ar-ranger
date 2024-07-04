using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the teleportation process triggered by a tile in the quiz scene.
/// </summary>
public class TileManager : MonoBehaviour
{
    [SerializeField] private QuizUIController _quizUIController;
    [SerializeField] private AudioClip _RangerVoiceOver;
    [SerializeField] private AudioSource _audioSource;
    private TeleportationManager _teleportationManager;
    private bool _shouldResumeTeleportation;
    private bool _clipCompleted;
    private InfoPathManager _infoPathManager;
    private GameObject _player;


    void OnEnable()
    {
        _shouldResumeTeleportation = false;
        _infoPathManager = InfoPathManager.Instance;
        _audioSource.clip = _RangerVoiceOver;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("XRRig");
        _teleportationManager = _player.GetComponentInChildren<TeleportationManager>();
    }

    // The duration of the voiceover determines the start of the teleportation, so the player is prepared
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _quizUIController.StopSelection();

            if (_shouldResumeTeleportation) { _audioSource.UnPause(); }
            else
            {
                _audioSource.Stop();
                _audioSource.Play();
            }

            StartCoroutine(checkPlaybackCompletion());
            StartCoroutine(ManageTeleportation());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(ManageTeleportation());
            StopCoroutine(checkPlaybackCompletion());
            StartCoroutine(ManageTiming());
        }
    }

    private IEnumerator checkPlaybackCompletion()
    {
        yield return new WaitForSeconds(_audioSource.clip.length - _audioSource.time);
        _clipCompleted = true;
    }

    // If the player just leaves the trigger quickliy and then comes back, the process shouldn't start all over again
    private IEnumerator ManageTiming()
    {
        _shouldResumeTeleportation = true;
        _audioSource.Pause();
        yield return new WaitForSeconds(5);
        _shouldResumeTeleportation = false;
    }

    private IEnumerator ManageTeleportation()
    {
        while (_audioSource.isPlaying)
        {
            // TODO: Am besten noch den "GazeIndicator" anzeigen, um den Zustand zu verdeutlichen und auf dem Onboarding aufzubauen.
            yield return null;
        }

        if (gameObject.GetComponent<Collider>().bounds.Contains(GameObject.FindGameObjectWithTag("Player").transform.position) &&
            _clipCompleted)
        {
            TeleportPlayer();
        }
        else yield return null;
    }

    private void TeleportPlayer()
    {
        // Hint: Every Quiz-Prefab must get the tag corresponding to the creature it is related to
        string QuizRelatedCreature = gameObject.transform.root.tag;
        InfoPathData infoPathData = _infoPathManager.InfoPathPositions[QuizRelatedCreature];

        _teleportationManager.GoSomeWhere(infoPathData.Position + new Vector3(0f, 10f, 0f), infoPathData.Rotation, Scenes.Main, TeleportationTrigger.Quiz);
    }
}
