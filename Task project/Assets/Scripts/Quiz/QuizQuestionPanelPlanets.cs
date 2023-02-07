using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;


public class QuizQuestionPanelPlanets : QuizQuestionPanel
{
	[SerializeField] private QuizQuestionPlanet _quizQuestionPlanetPrefab;

	protected override void LoadingAssetCompleted(AsyncOperationHandle<TextAsset> obj)
	{
		QuizData.QuestionsData questionsData = JsonUtility.FromJson<QuizData.QuestionsData>(obj.Result.text);
		Debug.Log("Loading assets complete planets");
		PanelFinish(-1);
		//GetRandomQuestions(questionsData.Questions);
	}

	protected override void GetRandomQuestions(List<QuizData.Question> questions)
	{
		if (questions.Count < _numberOfQuestionsToAsk)
		{
			//Debug.LogWarning("Not enough questions to ask in the list!");
			PanelFinish(-1);
			return;
		}


		// Rewwork GetRandomQuestions for diffreent question selection
		var random = new System.Random();
		for (int i = 1; i <= _numberOfQuestionsToAsk; i++)
		{
			int questionIndex = random.Next(questions.Count);
			_questionsToAsk.Enqueue(questions[questionIndex]);
			questions.RemoveAt(questionIndex);
		}

		CreateQuestion();
	}

	protected override void CreateQuestion()
	{
		QuizQuestionPlanet questionPlanet = Instantiate(_quizQuestionPlanetPrefab, transform.parent);
		questionPlanet.InitializeQuestion(_questionsToAsk.Dequeue(), GetAnswer);
	}

}
