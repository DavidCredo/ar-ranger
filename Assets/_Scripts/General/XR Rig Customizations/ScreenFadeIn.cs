using UnityEngine;

public class ScreenFadeIn : MonoBehaviour
{
    [SerializeField] private TransitionAnimator _transitionAnimator;

    void Start()
    {
        _transitionAnimator.FadeIn();
    }
}
