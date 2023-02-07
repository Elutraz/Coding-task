using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class QuizQuestionPlanet : QuizQuestion
{
	public override void InitializeQuestion(QuizData.Question questionRecievend, Action<bool> onComplete)
	{
		_questionTextMesh.text = questionRecievend.QuestionText;

		var random = new System.Random();
		_answerButtons = _answerButtons.OrderBy(x => random.Next()).ToList();

		for (int i = 0; i < _answerButtons.Count; i++)
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
}
