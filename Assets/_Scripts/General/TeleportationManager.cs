using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages teleportation functionality in the game.
/// </summary>
public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private TransitionAnimator TransitionAnimator;

    // Hint: Define all scences where the player GameObject does not get destroyed automatically after the scene was left.
    private string[] scenesToDestroyPlayer = { Scenes.LighthouseBasement, Scenes.LighthouseBalcony };

    public void GoSomeWhere(Vector3 position, Quaternion rotation, string sceneName, TeleportationTrigger teleportationTrigger)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        TeleportationPlacesStack.Instance.AddTeleportationPlace(new TeleportationPlace { Position = player.transform.position, Rotation = player.transform.rotation });
        TeleportationPlacesStack.Instance.AddTeleportationPlace(new TeleportationPlace { Position = position, Rotation = rotation });
        TeleportationPlacesStack.Instance.LastSceneName = SceneManager.GetActiveScene().name;
        TeleportationPlacesStack.Instance.LastTeleportationTrigger = teleportationTrigger;

        LoadScene(sceneName);
    }

    public void ReturnToLastPlace()
    {
        if (TeleportationPlacesStack.Instance.IsEmpty())
        {
            Debug.LogError("No place to return to");
            return;
        }

        string placeToGo = TeleportationPlacesStack.Instance.GetLastTeleportationPlace().SceneName;
        TeleportationPlacesStack.Instance.LastSceneName = SceneManager.GetActiveScene().name;
        TeleportationPlacesStack.Instance.LastTeleportationTrigger = TeleportationTrigger.Automation;

        LoadScene(placeToGo);
    }

    private void LoadScene(string sceneName)
    {
        foreach (string scene in scenesToDestroyPlayer)
        {
            if (SceneManager.GetActiveScene().name == scene)
            {
                TransitionAnimator.FadeOut();
                Destroy(GameObject.FindGameObjectWithTag("XRRig"));
            }
        }

        SceneManager.LoadScene(sceneName);
    }
}
