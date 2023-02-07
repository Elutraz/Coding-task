using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class QuizQuestion : MonoBehaviour
{
	[SerializeField] protected TextMeshProUGUI _questionTextMesh;

	[SerializeField] protected List<QuizAnswerButton> _answerButtons = new List<QuizAnswerButton>();

	protected int _buttonIndexOfCorrectAnswer = -1;

	protected Action<bool> _onAnswerRecieved;

	protected void OnAnswerButtonSelected(QuizAnswerButton quizAnswerButton)
	{
		_onAnswerRecieved.Invoke(quizAnswerButton == _answerButtons[_buttonIndexOfCorrectAnswer]);
		Destroy(gameObject);
	}

	public virtual void InitializeQuestion(QuizData.Question questionRecievend, Action<bool> onComplete) { }
}
