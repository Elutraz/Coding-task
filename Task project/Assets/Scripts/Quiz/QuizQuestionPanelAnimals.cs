using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;


public class QuizQuestionPanelAnimals : QuizQuestionPanel
{
	[SerializeField] private QuizQuestionAnimal _quizQuestionAnimalPrefab;

	private Queue<Question> _questionsToAsk = new Queue<Question>();

	protected override void LoadingAssetCompleted(AsyncOperationHandle<TextAsset> obj)
	{
		QuestionsData questionsData = JsonUtility.FromJson<QuestionsData>(obj.Result.text);
		//ListOutQuestions(questionsData.Questions);
		GetRandomQuestions(questionsData.Questions);
	}

	private void GetRandomQuestions(List<Question> questions)
	{
		if (questions.Count < _numberOfQuestionsToAsk)
		{
			//Debug.LogWarning("Not enough questions to ask in the list!");
			PanelFinish(-1);
			return;
		}

		var random = new System.Random();
		for (int i = 1; i <= _numberOfQuestionsToAsk; i++)
		{
			int questionIndex = random.Next(questions.Count);
			_questionsToAsk.Enqueue(questions[questionIndex]);
			questions.RemoveAt(questionIndex);
		}

		CreateQuestion();
	}

	private void CreateQuestion()
	{
		QuizQuestionAnimal questionAnimal = Instantiate(_quizQuestionAnimalPrefab, transform.parent);
		questionAnimal.InitializeQuestion(_questionsToAsk.Dequeue(), GetAnswer);
	}

	private void GetAnswer(bool correctAnswer)
	{
		if (correctAnswer)
		{
			_correctAnsweredQuestions++;
		}

		if (_questionsToAsk.Count == 0)
		{
			PanelFinish(_correctAnsweredQuestions);
			return;
		}

		CreateQuestion();
	}

	private void ListOutQuestions(List<Question> questions)
	{
		foreach(var question in questions)
		{
			Debug.Log("Question number: " + question.QuestionNumber + " Question text: " + question.QuestionText + " Correct answer: " + question.CorrectAnswer);
		}
	}

	[Serializable]
	public struct Question
	{
		public int QuestionNumber;
		public string QuestionText;
		public List<QuestionAnswers> Answers;
		public string CorrectAnswer;
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
