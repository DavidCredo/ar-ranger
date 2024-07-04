using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the behavior when the player leaves the lighthouse basement.
/// </summary>
public class LeaveLighthouseBasement : MonoBehaviour
{
    [SerializeField] private Vector3 _playerPositionInMainScene = new Vector3(-150f, 14.6f, -133f);
    [SerializeField] private Vector3 _playerRotationInMainScene = new Vector3(0f, 45f, 0f);

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TeleportationPlacesStack.Instance.LastSceneName = Scenes.LighthouseBasement;
            TeleportationPlacesStack.Instance.LastTeleportationTrigger = TeleportationTrigger.Door;
            TeleportationPlacesStack.Instance.AddTeleportationPlace(new TeleportationPlace { Position = _playerPositionInMainScene, Rotation = Quaternion.Euler(_playerRotationInMainScene), SceneName = Scenes.Main });
            Destroy(GameObject.FindGameObjectWithTag("XRRig"));
            SceneManager.LoadScene(Scenes.Main);
        }
    }
}
