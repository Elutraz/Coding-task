using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuizDonePanel : MonoBehaviour
{
	[SerializeField] private Button _tryAgainButton;

	[SerializeField] private TextMeshProUGUI _textMesh;

   public void ShowQuizResult(int correctAnswers, int totalQuestions)
	{
		gameObject.SetActive(true);

		if(correctAnswers == -1)
		{
			WrongResult();
			return;
		}

		decimal percentageCorrect = Math.Round((decimal) (correctAnswers * 100f / totalQuestions), 2);
		_textMesh.text = "Corrent answers \\ total questions\n" + correctAnswers + " \\ " + totalQuestions + "\nPercentage correct: " + percentageCorrect + " %";
	}

	private void WrongResult()
	{
		_tryAgainButton.gameObject.SetActive(false);
		_textMesh.text = "Something went wrong.\nClick main menu button.";
	}
}
