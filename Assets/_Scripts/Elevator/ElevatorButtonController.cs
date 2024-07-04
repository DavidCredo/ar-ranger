using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of elevator buttons in the game.
/// Activates the buttons as the player answers questions.
/// </summary>
public class ElevatorButtonController : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private List<QuizQuestionSO> questions;
    [field: SerializeField] public bool IsElevatorMoving { get; set; }

    private EventBinding<QuestionAnsweredEvent> _questionAnsweredEventBinding;

    void OnEnable()
    {
        _questionAnsweredEventBinding = new EventBinding<QuestionAnsweredEvent>(OnQuestionAnswered);
        EventBus<QuestionAnsweredEvent>.Register(_questionAnsweredEventBinding);

        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
        buttons[0].SetActive(true);
    }

    void OnDisable()
    {
        EventBus<QuestionAnsweredEvent>.Unregister(_questionAnsweredEventBinding);
    }

    private void OnQuestionAnswered(QuestionAnsweredEvent e)
    {
        buttons[questions.IndexOf(e.Question) + 1].SetActive(true);
    }

}


public struct QuestionAnsweredEvent : IEvent
{
    public QuizQuestionSO Question;
}

