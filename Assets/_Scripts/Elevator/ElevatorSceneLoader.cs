using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorSceneLoader : MonoBehaviour
{
    void Awake()
    {
        if (!SceneManager.GetSceneByName(Scenes.Elevator).isLoaded)
        {
            SceneManager.LoadScene(Scenes.Elevator, LoadSceneMode.Additive);
        }
    }
}
