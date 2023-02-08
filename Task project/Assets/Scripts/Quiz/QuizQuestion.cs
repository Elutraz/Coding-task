using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class QuizQuestion : MonoBehaviour
{
	[SerializeField] protected TextMeshProUGUI _questionTextMesh;

	[SerializeField] protected List<QuizAnswerButton> _answerButtons = new List<QuizAnswerButton>();

	protected QuizAnswerButton _correctAnswerButton = null;

	protected Action<bool> _onAnswerRecieved;

	protected void OnAnswerButtonSelected(QuizAnswerButton quizAnswerButton)
	{
		_onAnswerRecieved.Invoke(quizAnswerButton == _correctAnswerButton);
		Destroy(gameObject);
	}

	public virtual void InitializeQuestion(QuizData.Question questionRecievend, Action<bool> onComplete) { }
}
