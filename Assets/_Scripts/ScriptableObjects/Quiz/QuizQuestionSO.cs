using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Quiz Question", menuName = "QuizSystem/Question")]
public class QuizQuestionSO : ScriptableObject
{
    public string question;
    public List<string> answers;
    public int correctAnswerIndex;
    public List<int> selectedAnswers { get; set; }
    public bool IsAlreadyAnswered { get; set; }
    // public List<Image> picturesToFrame;
    public string congratulationText;

    void OnEnable()
    {
        IsAlreadyAnswered = false;
        selectedAnswers = new List<int>();
    }
}
