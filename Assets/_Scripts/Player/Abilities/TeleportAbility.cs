using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents a teleport ability that activates and deactivates a locomotion system.
/// </summary>
public class TeleportAbility : MonoBehaviour
{
    [SerializeField] private LocomotionSystem _locomotionSystem;
    public AbilitySO teleportAbilitySO;

    void OnEnable()
    {
        teleportAbilitySO.onActivate += OnActivate;
        teleportAbilitySO.onDeactivate += OnDeactivate;
    }

    void OnDisable()
    {
        teleportAbilitySO.onActivate -= OnActivate;
        teleportAbilitySO.onDeactivate -= OnDeactivate;
    }

    public void OnActivate()
    {
        _locomotionSystem.gameObject.SetActive(true);
    }

    public void OnDeactivate()
    {
        _locomotionSystem.gameObject.SetActive(false);
    }

}
