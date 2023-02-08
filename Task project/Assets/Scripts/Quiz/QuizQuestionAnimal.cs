using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class QuizQuestionAnimal : QuizQuestion
{
	public override void InitializeQuestion(QuizData.Question questionRecievend, Action<bool> onComplete)
	{
		_questionTextMesh.text = questionRecievend.QuestionText;

		int correctAnswerIndex = questionRecievend.CorrectAnswerNumber - 1;

		var random = new System.Random();
		_answerButtons = _answerButtons.OrderBy(x => random.Next()).ToList();

		for (int i = 0; i < questionRecievend.Answers.Count; i++)
		{
			string answer = questionRecievend.Answers[i].Answer;
			_answerButtons[i].InitializeButton(answer, OnAnswerButtonSelected);

			if (_correctAnswerButton == null && correctAnswerIndex == i)
			{
				_correctAnswerButton = _answerButtons[i];
			}
		}

		_onAnswerRecieved += onComplete;
	}
}
