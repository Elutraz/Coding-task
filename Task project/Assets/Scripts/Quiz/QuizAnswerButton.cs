using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuizAnswerButton : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _textMesh;

	private Action<QuizAnswerButton> _onButtonClicked;

	public void InitializeButton(string answer, Action<QuizAnswerButton> onButtonClick)
	{
		_textMesh.text = answer;
		_onButtonClicked += onButtonClick;
	}

	public void ButtonClicked()
	{
		_onButtonClicked.Invoke(this);
	}
}
