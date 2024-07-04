using UnityEngine;

/// <summary>
/// Coordinates the spawning of creatures based on the last teleportation trigger and scene name.
/// </summary>
public class CreatureSpawnCoordinator : MonoBehaviour
{
    [SerializeField] private CreatureSpawner _musselSpawner;
    [SerializeField] private CreatureSpawner _schulpSpawner;
    [SerializeField] private CreatureSpawner _salzmierenSpawner;

    void OnEnable()
    {
        if (TeleportationPlacesStack.Instance.LastTeleportationTrigger == TeleportationTrigger.Quiz)
        {
            switch (TeleportationPlacesStack.Instance.LastSceneName)
            {
                case Scenes.LighthouseBasement:
                    _salzmierenSpawner.PlaceCreatures();
                    break;
                case Scenes.Lighthouse1st:
                    Debug.Log("1st floor are Sanderlings, they don't need to be placed");
                    break;
                case Scenes.Lighthouse2nd:
                    _musselSpawner.PlaceCreatures();
                    break;
                case Scenes.Lighthouse3rd:
                    _schulpSpawner.PlaceCreatures();
                    break;
                default:
                    break;
            }

        }
    }
}
