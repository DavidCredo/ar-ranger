using System.Collections.Generic;
using UnityEngine;
using ARRanger;

public class QuestionDataContainer : PersistentSingleton<QuestionDataContainer>
{
    [SerializeField] private List<QuizQuestionSO> _questions = new List<QuizQuestionSO>();
}
