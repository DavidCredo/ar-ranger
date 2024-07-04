using ARRanger;
using UnityEngine;

/// <summary>
/// Handles scene transitions triggered by a collider.
/// </summary>
public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private TransitionAnimator _transitionAnimator;

    private void OnEnable()
    {
        if (_transitionAnimator == null)
        {
            _transitionAnimator = FindObjectOfType<TransitionAnimator>();
        }
    }

    /// <summary>
    /// Transitions to the specified scene.
    /// </summary>
    void TransitionToScene()
    {
        _transitionAnimator.FadeOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
        EventBus<SceneTransitionEvent>.Raise(new SceneTransitionEvent());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionToScene();
        }
    }
}
