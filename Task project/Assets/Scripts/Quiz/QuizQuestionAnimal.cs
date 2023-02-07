using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class QuizQuestionAnimal : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _questionTextMesh;

	[SerializeField] private List<QuizAnswerButton> _answerButtons = new List<QuizAnswerButton>();

	private int _buttonIndexOfCorrectAnswer = -1;

	private Action<bool> _onAnswerRecieved;

	public void InitializeQuestion(QuizQuestionPanelAnimals.Question questionRecievend, Action<bool> onComplete)
	{
		_questionTextMesh.text = questionRecievend.QuestionText;

		var random = new System.Random();
		_answerButtons = _answerButtons.OrderBy(x => random.Next()).ToList();

		for (int i = 0; i < questionRecievend.Answers.Count; i++)
		{
			string answer = questionRecievend.Answers[i].Answer;
			_answerButtons[i].InitializeButton(answer, OnAnswerButtonSelected);

			if (_buttonIndexOfCorrectAnswer == -1 && answer == questionRecievend.CorrectAnswer)
			{
				_buttonIndexOfCorrectAnswer = i;
			}
		}

		_onAnswerRecieved += onComplete;

	}

	private void OnAnswerButtonSelected(QuizAnswerButton quizAnswerButton)
	{
		_onAnswerRecieved.Invoke(quizAnswerButton == _answerButtons[_buttonIndexOfCorrectAnswer]);
		Destroy(gameObject);
	}
}
