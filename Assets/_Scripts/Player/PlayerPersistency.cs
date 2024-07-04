using System.Linq.Expressions;
using ARRanger;
using UnityEngine;

/// <summary>
/// Represents a persistent singleton class that handles player persistency.
/// </summary>
public class PlayerPersistency : PersistentSingleton<PlayerPersistency>
{
    private EventBinding<SceneTransitionEvent> _sceneTransitionEvent;

    void OnEnable()
    {
        _sceneTransitionEvent = new EventBinding<SceneTransitionEvent>(OnSceneTransition);
        EventBus<SceneTransitionEvent>.Register(_sceneTransitionEvent);
    }

    void OnDisable()
    {
        EventBus<SceneTransitionEvent>.Unregister(_sceneTransitionEvent);
    }

    void OnSceneTransition(SceneTransitionEvent sceneTransitionEvent)
    {
        Debug.Log("Destroying Player, SceneTransitionEvent received.");
        Destroy(gameObject);
    }

}
