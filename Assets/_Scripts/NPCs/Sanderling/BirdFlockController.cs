using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the behavior of a bird flock in the game.
/// </summary>
public class BirdFlockController : MonoBehaviour
{
    [SerializeField]
    private float _idleTime = 1f;
    private List<NavMeshAgent> _birdAgents = new List<NavMeshAgent>();

    [SerializeField]
    private Transform _birdTravelArea;

    [SerializeField]
    private GameObject _birdPrefab;

    [SerializeField]
    private int _birdCount = 20;
    private GameStateMachine _birdFlockStateMachine;

    public GameStateMachine BirdFlockStateMachine { get => _birdFlockStateMachine; set => _birdFlockStateMachine = value; }
    public List<NavMeshAgent> BirdAgents { get => _birdAgents; }
    public float IdleTime { get => _idleTime; set => _idleTime = value; }


    void OnEnable()
    {
        for (int i = 0; i < _birdCount; i++)
        {
            GameObject bird = Instantiate(_birdPrefab, transform.position, Quaternion.identity);
            _birdAgents.Add(bird.GetComponent<NavMeshAgent>());
        }
        _birdFlockStateMachine = new GameStateMachine(null);
        _birdFlockStateMachine.ChangeState(new BirdFlockIdleState(this, _birdFlockStateMachine));
    }
    // Update is called once per frame
    void Update()
    {
        BirdFlockStateMachine.Update();
    }

    /// <summary>
    /// Sets a random destination for each bird agent within the bird travel area.
    /// </summary>
    public void SetRandomDestination()
    {
        float xBound = _birdTravelArea.localScale.x / 2;
        float zBound = _birdTravelArea.localScale.z / 2;
        Vector2 randomPointOnArea = new Vector2(Random.Range(_birdTravelArea.position.x - xBound, _birdTravelArea.position.x + xBound), Random.Range(_birdTravelArea.position.z - zBound, _birdTravelArea.position.z + zBound));

        foreach (NavMeshAgent agent in BirdAgents)
        {
            Vector2 variation = Random.insideUnitCircle * 2;
            Vector3 destination = new Vector3(randomPointOnArea.x + variation.x, agent.transform.position.y, randomPointOnArea.y + variation.y);
            agent.SetDestination(destination);
            agent.isStopped = false;
        }
    }

    /// <summary>
    /// Makes the birds in the flock idle by stopping their movement.
    /// </summary>
    public void LetBirdsIdle()
    {
        foreach (NavMeshAgent agent in BirdAgents)
        {
            agent.isStopped = true;
        }
    }
}
