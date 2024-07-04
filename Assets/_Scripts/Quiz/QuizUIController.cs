using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using Unity.XR.CoreUtils;

/// <summary>
/// Controls the user interface for the quiz, displaying questions and handling user answers.
/// </summary>
public class QuizUIController : MonoBehaviour
{
    #region Fields
    [SerializeField] private QuizQuestionSO question;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI[] answerTexts;
    [SerializeField] private GameObject QuestionUI;
    [SerializeField] private GameObject AnswerUI;
    [SerializeField] private TextMeshProUGUI AnswerText;
    [SerializeField] private GameObject[] _pictureFrames;
    [SerializeField] private Texture2D[] _picturesToFrame;
    [SerializeField] private GameObject _selectionIndicator;
    [SerializeField] private GameObject[] _tiles;
    private Tween _fillTween;
    private Tween _emptyTween;

    #endregion

    void Start()
    {
        if (question.IsAlreadyAnswered) OnCorrectAnswerSelected();
        else
        {
            questionText.text = question.question;
            for (int i = 0; i < answerTexts.Length; i++)
            {
                answerTexts[i].text = question.answers[i];
            }

            for (int i = 0; i < _pictureFrames.Length; i++)
            {
                _pictureFrames[i].GetComponent<Renderer>().material.SetTexture("_BaseMap", _picturesToFrame[i]);
            }

            foreach (var selection in question.selectedAnswers)
            {
                if (selection != question.correctAnswerIndex)
                {
                    MarkWrong(selection);
                }
            }

        }
    }

    public void StartSelection(int answerIndex)
    {
        StartCoroutine(SelectionProgress(answerIndex));
    }

    public void StopSelection()
    {
        if (_selectionIndicator.activeSelf) _fillTween.Kill();

        ResetSelectionIndicator();
    }

    private IEnumerator SelectionProgress(int answerIndex)
    {
        while (_emptyTween.IsActive()) yield return null;

        _selectionIndicator.transform.position = answerTexts[answerIndex].transform.position + new Vector3(0, 0.08f, 0);

        _selectionIndicator.SetActive(true);

        _fillTween = _selectionIndicator.GetComponent<Image>().DOFillAmount(1, 4).Play().OnComplete(() =>
        {
            _selectionIndicator.SetActive(false);
            OnAnswerSelected(answerIndex);
        });
    }

    private void ResetSelectionIndicator()
    {
        _emptyTween = _selectionIndicator.GetComponent<Image>().DOFillAmount(0, 1).Play().OnComplete(() => _selectionIndicator.SetActive(false));
    }

    /// <summary>
    /// Handles the selection of an answer in the quiz UI.
    /// </summary>
    /// <param name="selectedAnswerIndex">The index of the selected answer.</param>
    private void OnAnswerSelected(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == question.correctAnswerIndex) OnCorrectAnswerSelected();
        else OnWrongAnswerSelected(selectedAnswerIndex);
    }

    private void OnWrongAnswerSelected(int selectedAnswerIndex)
    {
        question.selectedAnswers.Add(selectedAnswerIndex);
        MarkWrong(selectedAnswerIndex);
    }

    private void OnCorrectAnswerSelected()
    {
        question.IsAlreadyAnswered = true;
        foreach (var tile in _tiles) tile.SetActive(false);
        AnswerText.text = question.congratulationText;
        UncoverPictures();
        QuestionUI.SetActive(false);
        AnswerUI.SetActive(true);
        EventBus<QuestionAnsweredEvent>.Raise(new QuestionAnsweredEvent { Question = question });
    }

    private void MarkWrong(int answerIndex)
    {
        answerTexts[answerIndex].text = "<s>" + answerTexts[answerIndex].text + "</s>";
        _tiles[answerIndex].GetNamedChild("3DText").SetActive(false);
        _tiles[answerIndex].GetNamedChild("2DText").GetComponent<TextMeshPro>().fontSize = 24;
        _tiles[answerIndex].GetNamedChild("2DText").GetComponent<TextMeshPro>().text = "<i><s> " + _tiles[answerIndex].GetNamedChild("2DText").GetComponent<TextMeshPro>().text + "  </s></i>";
    }

    private void UncoverPictures()
    {
        foreach (var frame in _pictureFrames)
        {
            frame.SetActive(true);
        }
    }
}
