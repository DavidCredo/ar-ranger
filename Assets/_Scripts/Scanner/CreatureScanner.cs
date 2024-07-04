using System.Collections;
using ARRanger.DependencyInjection;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a scanner used to scan for creatures in a game.
/// </summary>
public class CreatureScanner : MonoBehaviour, IDependencyProvider
{

    #region Private Fields
    [SerializeField]
    private LayerMask _creatureLayerMask;

    [SerializeField]
    [Tooltip("The maximum distance the scanner can scan.")]
    private float _maxScanDistance = 10f;

    [SerializeField]
    [Tooltip("The duration of the scanning process.")]
    private float _scanningDuration = 1f;

    [SerializeField] private AudioSource _scanAudioSource;

    [SerializeField]
    private ScanLaserController _scanLaserController;
    private IState _idleState;

    private Creature _currentTarget;

    private Camera _mainCamera;

    public GameStateMachine StateMachine { get; private set; }

    #endregion

    void Awake()
    {
        _idleState = new ScannerIdleState(this);
        StateMachine = new GameStateMachine(_idleState);
    }

    void OnEnable()
    {
        _mainCamera = Camera.main;
    }

    [Provide]
    CreatureScanner ProvideCreatureScanner()
    {
        return this;
    }

    void Update()
    {
        StateMachine.Update();
    }

    /// <summary>
    /// Starts the scanning process for a creature when it is hit by a raycast.
    /// </summary>
    /// <param name="hit">The RaycastHit object containing information about the hit.</param>
    private void StartCreatureScan(RaycastHit hit)
    {
        if (hit.collider.gameObject.TryGetComponent<Creature>(out var creature) && !creature.Data.AlreadyScanned)
        {
            _scanLaserController.SetScanningState(hit.distance);
            _currentTarget = creature;
            StartCoroutine(ScanCreature(creature));
            StateMachine.ChangeState(new ScanningState());
        }
        else
        {
            _scanLaserController.SetLaserHitPoint(hit.distance);
        }
    }

    private IEnumerator ScanCreature(Creature creature)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _scanningDuration)
        {
            float progress = Mathf.Clamp01(elapsedTime / _scanningDuration);
            EventBus<ScanEvent>.Raise(new ScanEvent(progress, creature.Data, false));
            elapsedTime += Time.deltaTime;

            if (!IsStillScanningCreature())
            {
                AbortScan();
                yield break;
            }
            else if (elapsedTime >= _scanningDuration)
            {
                CompleteCreatureScan(creature.Data, creature.transform);
            }
            yield return null;
        }
    }

    private void AbortScan()
    {
        StateMachine.ChangeState(_idleState);
        EventBus<ScanEvent>.Raise(new ScanEvent(scanProgress: 0f, _currentTarget.Data, isAborting: true));
        _scanLaserController.ResetLaser();
    }

    private bool IsStillScanningCreature()
    {
        return Physics.Raycast(
            transform.position,
            transform.forward,
            out RaycastHit hit,
            _maxScanDistance,
            _creatureLayerMask
        ) && hit.collider.gameObject.TryGetComponent<Creature>(out var creature) && creature.Data == _currentTarget.Data;
    }

    private void CompleteCreatureScan(ICreature creature, Transform creatureTransform)
    {
        creature.AlreadyScanned = true;
        creature.ScannedAtPosition = creatureTransform.position;
        _currentTarget = null;
        _scanAudioSource.Play();
        _scanLaserController.ResetLaser();
        EventBus<ScanEvent>.Raise(new ScanEvent(scanProgress: 1f, creature, isAborting: false));
        StateMachine.ChangeState(_idleState);
    }

    /// <summary>
    /// Checks if the user is aiming the scanner in the forward direction.
    /// </summary>
    /// <returns>True if the user is aiming the scanner, false otherwise.</returns>
    public bool IsUserAimingScanner()
    {
        Vector3 scannerDirection = transform.forward;

        Vector3 playerViewDirection = _mainCamera.transform.forward;

        float dotProduct = Vector3.Dot(scannerDirection.normalized, playerViewDirection.normalized);

        return dotProduct > 0.9f;
    }

    /// <summary>
    /// Scans for creatures in front of the scanner' transform position.
    /// </summary>
    public void ScanForCreatures()
    {
        Debug.DrawRay(transform.position, transform.forward * _maxScanDistance, Color.red);
        if (Physics.Raycast(
            transform.position,
            transform.forward,
            out RaycastHit hit,
            _maxScanDistance,
            _creatureLayerMask
                            )
            )
        {
            StartCreatureScan(hit);
        }
        else
        {
            _scanLaserController.ResetLaser();
        }
    }

    public void DisableLaser()
    {
        _scanLaserController.DisableLaser();
    }

    public void EnableLaser()
    {
        _scanLaserController.EnableLaser();
    }
}
