using DG.Tweening;
using UnityEngine;

public class ElevatorAnimation : MonoBehaviour
{
    [SerializeField]
    float _duration = 2f;

    [SerializeField] private AudioSource _elevatorDoorOpenSound;

    [SerializeField] private AudioSource _elevatorDoorCloseSound;

    [SerializeField] private Transform _leftDoorRotateAnchor;
    [SerializeField] private Transform _rightDoorRotateAnchor;
    private Sequence _openElevatorSequence;
    private Sequence _closeElevatorSequence;

    void Start()
    {

        //TODO: Move into own dotween manager
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;

        OpenElevatorDoors();
    }



    /// <summary>
    /// Rotates the associated elevator doors to open them.
    /// </summary>
    /// <returns>The created sequence, starting it.</returns>
    public Sequence OpenElevatorDoors()
    {
        _openElevatorSequence = DOTween.Sequence();
        _openElevatorSequence.AppendInterval(2f);
        _openElevatorSequence.AppendCallback(() => _elevatorDoorOpenSound.Play());
        _openElevatorSequence.Join(_leftDoorRotateAnchor.DOLocalRotate(new Vector3(0, 25f, 0), 4f));
        _openElevatorSequence.Join(_rightDoorRotateAnchor.DOLocalRotate(new Vector3(0, -25f, 0), 4f));
        return _openElevatorSequence.Play();
    }

    /// <summary>
    /// Rotates the associated elevator doors to close them.
    /// </summary>
    /// <returns>The created sequence, starting it.</returns>
    public Sequence CloseElevatorDoors()
    {
        _closeElevatorSequence = DOTween.Sequence();
        _closeElevatorSequence.AppendCallback(() => _elevatorDoorCloseSound.Play());
        _closeElevatorSequence.Join(_leftDoorRotateAnchor.DOLocalRotate(new Vector3(0, 0, 0), _duration));
        _closeElevatorSequence.Join(_rightDoorRotateAnchor.DOLocalRotate(new Vector3(0, 0, 0), _duration));

        return _closeElevatorSequence.Play();
    }
}
