using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an object pool for creatures in the game.
/// </summary>
public class CreatureObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _creaturePrefab;

    [SerializeField]
    private int _poolSize = 100;

    [SerializeField]
    private List<Transform> _spawnRegions = new List<Transform>();

    private List<GameObject> _creaturePool = new List<GameObject>();

    public List<Transform> SpawnRegions { get => _spawnRegions; set => _spawnRegions = value; }
    public GameObject CreaturePrefab { get => _creaturePrefab; private set => _creaturePrefab = value; }

    void Awake()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject creature = Instantiate(CreaturePrefab, transform);
            creature.SetActive(false);
            _creaturePool.Add(creature);
        }
    }

    /// <summary>
    /// Retrieves a pooled creature from the object pool.
    /// </summary>
    /// <returns>The pooled creature as a GameObject.</returns>
    public GameObject GetPooledCreature()
    {
        foreach (GameObject creature in _creaturePool)
        {
            if (!creature.activeInHierarchy)
            {
                return creature;
            }
        }

        GameObject newCreature = Instantiate(CreaturePrefab);
        newCreature.SetActive(false);
        _creaturePool.Add(newCreature);
        return newCreature;
    }
}
