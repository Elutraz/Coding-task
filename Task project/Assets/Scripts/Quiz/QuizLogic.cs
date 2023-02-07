using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuizLogic : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private List<GameObject> _hideObjectsOnResultShown = new List<GameObject>();

	[SerializeField] private QuizDonePanel _quizDonePanel;

	[Space, Header("Variables")]
	[SerializeField] private QuestionType _questionsType;

	[SerializeField] private int _numberOfQuestions = 10;

	private void Start()
	{
		StartQuestions();
	}

	public void StartQuestions()
	{
		ToggleObjectsOnResultShown(true);
		QuizQuestionPanel panelObject = null;

		if (QuizData.Instance.QuizQuestionsDictionary.TryGetValue(_questionsType, out panelObject) && panelObject != null)
		{
			panelObject = Instantiate(panelObject, transform);
			panelObject.InitializePanel(_numberOfQuestions, ShowResultPanel);
		}
		else
		{
			ShowResultPanel(-1);
		}
	}

	private void ShowResultPanel(int correctQuestionsAnswered)
	{
		ToggleObjectsOnResultShown(false);
		_quizDonePanel.ShowQuizResult(correctQuestionsAnswered, _numberOfQuestions);
	}

	private void ToggleObjectsOnResultShown(bool activate)
	{
		foreach (var hideObject in _hideObjectsOnResultShown.Where(x => x.activeInHierarchy != activate))
		{
			hideObject.SetActive(activate);
		}
	}


	public enum QuestionType
	{
		None,
		Animals,
		Planets,
	}

	[System.Serializable]
	public struct QuestionData
	{
		public QuestionType Type;
		public QuizQuestionPanel PanelPrefab;
	}
}
