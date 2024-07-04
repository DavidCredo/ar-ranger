using UnityEngine;

/// <summary>
/// Spawns creatures in a specified region based on a creature object pool and terrain.
/// </summary>
public class CreatureSpawner : MonoBehaviour
{
    [SerializeField]
    private CreatureObjectPool _creatureObjectPool;

    [SerializeField]
    private Terrain _terrain;

    [SerializeField]
    private float _creatureDensity = 1f;

    [SerializeField]
    private int _creatureCount = 100;

    [SerializeField] private float _creatureHeightOffset = 0f;

    [SerializeField] private bool _wereCreaturesPlaced = false;


    void Start()
    {
        if (TeleportationPlacesStack.Instance.LastTeleportationTrigger == TeleportationTrigger.Quiz)
        {

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_wereCreaturesPlaced)
        {
            Debug.Log("CreatureSpawner.OnTriggerEnter: Placing creatures -> " + _creatureObjectPool.CreaturePrefab.name);
            PlaceCreatures();
        }
    }

    /// <summary>
    /// Places the creatures in the spawn regions, if they were not already placed.
    /// </summary>
    public void PlaceCreatures()
    {
        if (_wereCreaturesPlaced)
        {
            return;
        }

        foreach (Transform spawnRegion in _creatureObjectPool.SpawnRegions)
        {
            float spawnRegionXSize = spawnRegion.localScale.x / 2;
            float spawnRegionZSize = spawnRegion.localScale.z / 2;
            int creatureCountInRegion = _creatureCount;

            for (int i = 0; i < creatureCountInRegion; i++)
            {
                GameObject creature = _creatureObjectPool.GetPooledCreature();
                Vector3 randomOffset = new Vector3(Random.Range(-spawnRegionXSize, spawnRegionXSize), 0, Random.Range(-spawnRegionZSize, spawnRegionZSize));

                Vector3 randomPosition = spawnRegion.position + randomOffset;

                Vector3 terrainAdjustedPosition = new Vector3(randomPosition.x, GetTerrainHeightAtSpawnPosition(randomPosition) + _creatureHeightOffset, randomPosition.z);
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0, 360f), 0);

                creature.transform.SetPositionAndRotation(terrainAdjustedPosition, randomRotation);
                creature.SetActive(true);
            }
        }
        _wereCreaturesPlaced = true;
    }

    private float GetTerrainHeightAtSpawnPosition(Vector3 spawnPosition)
    {
        return _terrain.SampleHeight(spawnPosition);
    }
}
