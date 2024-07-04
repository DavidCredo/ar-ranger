using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a tile that triggers a quiz answer selection when the player enters its collider.
/// </summary>
public class QuizAnswerTile : MonoBehaviour
{
    [SerializeField] private QuizUIController _quizUIController;
    [SerializeField] private int _answerIndex;

    /// <summary>
    /// Called when another collider enters this tile's collider.
    /// Starts the quiz answer selection process.
    /// </summary>
    /// <param name="other">The collider that entered this tile's collider.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _quizUIController.StartSelection(_answerIndex);
        }
    }

    /// <summary>
    /// Called when another collider exits this tile's collider.
    /// Stops the quiz answer selection process.
    /// </summary>
    /// <param name="other">The collider that exited this tile's collider.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _quizUIController.StopSelection();
        }
    }
}