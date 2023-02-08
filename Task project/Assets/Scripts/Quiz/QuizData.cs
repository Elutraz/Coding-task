using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class QuizData : MonoBehaviour
{
	public static QuizData Instance = null;

	[SerializeField] private List<QuizLogic.QuestionData> _quizQuestionsData = new List<QuizLogic.QuestionData>();

	private Dictionary<QuizLogic.QuestionType, QuizQuestionPanel> _quizQuestionsDictionary = new Dictionary<QuizLogic.QuestionType, QuizQuestionPanel>();

	public Dictionary<QuizLogic.QuestionType, QuizQuestionPanel> QuizQuestionsDictionary => _quizQuestionsDictionary;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			Initialize();
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Initialize()
	{
		_quizQuestionsDictionary = _quizQuestionsData.ToDictionary(x => x.Type, x => x.PanelPrefab);
	}

	[Serializable]
	public struct Question
	{
		public int QuestionNumber;
		public string QuestionText;
		public List<QuestionAnswers> Answers;
		public int CorrectAnswerNumber;
	}

	[Serializable]
	public struct QuestionsData
	{
		public List<Question> Questions;
	}

	[Serializable]
	public struct QuestionAnswers
	{
		public string Answer;
	}
}
