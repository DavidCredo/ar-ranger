using System.Collections.Generic;
using ARRanger.DependencyInjection;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class InfoPathPlacement : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _infoPaths = new List<GameObject>();
    private CreatureDataContainer _creatureDataContainer;

    private InfoPathManager _infoPathManager;

    private EventBinding<ScanEvent> _scanEventBinding;
    private Transform _playerView;
    private GameObject _matchingInfoPath;

    void OnEnable()
    {
        _infoPathManager = InfoPathManager.Instance;
        _creatureDataContainer = CreatureDataContainer.Instance;

        _scanEventBinding = new EventBinding<ScanEvent>(OnScanEvent);
        EventBus<ScanEvent>.Register(_scanEventBinding);
        PositionDiscoveredInfoPaths();
    }

    void OnDisable()
    {
        EventBus<ScanEvent>.Unregister(_scanEventBinding);
    }

    private void OnScanEvent(ScanEvent scanEvent)
    {
        if (scanEvent.IsComplete)
        {
            _playerView = Camera.main.transform;

            foreach (var infoPath in _infoPaths)
            {
                if (infoPath.CompareTag(scanEvent.CreatureData.Name))
                {
                    _matchingInfoPath = infoPath;
                }
            }

            SpawnInfoPath(_matchingInfoPath, scanEvent.CreatureData);
        }
    }

    private void SpawnInfoPath(GameObject infoPath, ICreature creatureData)
    {
        infoPath.transform.position = creatureData.ScannedAtPosition.Value + new Vector3(0, -10, 0);

        Vector3 playerToCreatureDirection = creatureData.ScannedAtPosition.Value.With(y: 0) - _playerView.transform.position.With(y: 0);

        infoPath.transform.position += playerToCreatureDirection.normalized * 2;

        infoPath.transform.LookAt(_playerView.transform.position.With(y: infoPath.transform.position.y));

        infoPath.transform.DOMoveY(_playerView.transform.position.y - 0.5f, 5f).Play();

        NotifiyPathManager(infoPath, _playerView.transform.position.y);
    }

    private void NotifiyPathManager(GameObject infoPath, float playerYOffset)
    {
        InfoPathData infoPathData = new InfoPathData { Position = infoPath.transform.position, Rotation = infoPath.transform.rotation, PlayerYOffset = playerYOffset };
        _infoPathManager.AddInfoPathPosition(infoPath.tag, infoPathData);
    }

    private void PositionDiscoveredInfoPaths()
    {
        foreach (var creature in _creatureDataContainer.Creatures)
        {
            if (creature.AlreadyScanned)
            {
                foreach (var infoPath in _infoPaths)
                {
                    if (infoPath.CompareTag(creature.Name))
                    {
                        InfoPathData infoPathData = _infoPathManager.InfoPathPositions[creature.Name];
                        infoPath.transform
                        .SetPositionAndRotation
                        (
                            infoPathData.Position.With(y: infoPathData.PlayerYOffset - 0.25f),
                            infoPathData.Rotation
                        );

                    }
                }
            }
        }
    }
}
