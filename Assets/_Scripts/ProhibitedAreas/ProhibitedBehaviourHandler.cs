using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the behavior of the player when entering or exiting a prohibited area.
/// </summary>
class ProhibitedBehaviourHandler : MonoBehaviour {
    private EventBinding<ProhibitedAreaEvent> _prohibitedAreaEventBinding;
    private GameObject _player = GameObject.FindWithTag("Player");

    void OnEnable() {
        _prohibitedAreaEventBinding = new EventBinding<ProhibitedAreaEvent>(OnProhibitedAreaEvent);
        EventBus<ProhibitedAreaEvent>.Register(_prohibitedAreaEventBinding);
    }

    void OnDisable() {
        EventBus<ProhibitedAreaEvent>.Unregister(_prohibitedAreaEventBinding);
    }

    private void OnProhibitedAreaEvent(ProhibitedAreaEvent prohibitedAreaEvent) {
        if (prohibitedAreaEvent.IsInProhibitedArea) {
            StartCoroutine(teleportToSpawn(_player));
        } else {
            StopCoroutine(teleportToSpawn(_player));
        }
    }

    private IEnumerator teleportToSpawn(GameObject player)
    {
        // TODO: Needs announcement from Ranger (Audio) -> atm only text, see WarningMessageChanger.cs and WarningMessageEventListener.cs
        yield return new WaitForSeconds(20f);
        // TODO: Add fade out and fade in
        player.transform.position = new Vector3(0f, 0f, 0f);
    }
}