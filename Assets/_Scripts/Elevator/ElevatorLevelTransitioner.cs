using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the transition between elevator levels in the game.
/// </summary>
public class ElevatorLevelTransitioner : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;

    [SerializeField]
    private ElevatorAnimation _elevatorAnimation;

    [SerializeField] private AudioSource _elevatorAmbientSound;

    [SerializeField] private ElevatorButtonController _elevatorButtonController;

    /// <summary>
    /// Unloads a scene asynchronously.
    /// </summary>
    /// <param name="sceneToUnload">The scene to unload.</param>
    private IEnumerator UnloadSceneAsync(Scene sceneToUnload)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneToUnload);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Unloads non-persistent scenes.
    /// </summary>
    private void UnloadNonPersistentScenes()
    {
        Scene sceneToUnload;
        bool isElevatorLevelAlreadyLoaded = false;
        bool isSceneToLoadAlreadyLoaded = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name != "Level_Fahrstuhl" && scene.name != _sceneToLoad || (isElevatorLevelAlreadyLoaded && isSceneToLoadAlreadyLoaded))
            {
                sceneToUnload = scene;
                StartCoroutine(UnloadSceneAsync(sceneToUnload));
            }
            else
            {
                Debug.Log("Scene " + scene.name + " is persistent and will not be unloaded.");
            }

            if (scene.name == "Level_Fahrstuhl")
            {
                isElevatorLevelAlreadyLoaded = true;
            }
            else if (scene.name == _sceneToLoad)
            {
                isSceneToLoadAlreadyLoaded = true;
            }
        }
    }

    /// <summary>
    /// Transitions to the specified scene.
    /// </summary>
    public void TransitionToScene()
    {
        if (_elevatorButtonController.IsElevatorMoving)
        {
            Debug.Log("Elevator is already moving.");
            return;
        }
        else if (SceneManager.GetSceneByName(_sceneToLoad).isLoaded)
        {
            Debug.Log("Currently on this level, won't move.");
            return;
        }
        _elevatorButtonController.IsElevatorMoving = true;
        _elevatorAnimation.CloseElevatorDoors()
        .OnComplete(() =>
        {
            UnloadNonPersistentScenes();
            StartCoroutine(LoadSceneAsync(_sceneToLoad));
        });
    }

    /// <summary>
    /// Loads a scene asynchronously.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        _elevatorAmbientSound.Play();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        WaitForSeconds wait = new WaitForSeconds(3f);
        yield return wait;
        _elevatorAmbientSound.Stop();
        _elevatorAnimation.OpenElevatorDoors();
        _elevatorButtonController.IsElevatorMoving = false;
    }
}
