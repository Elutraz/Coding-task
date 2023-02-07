using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;


public class QuizQuestionPanelAnimals : QuizQuestionPanel
{
	[SerializeField] private QuizQuestionAnimal _quizQuestionAnimalPrefab;

	protected override void LoadingAssetCompleted(AsyncOperationHandle<TextAsset> obj)
	{
		QuizData.QuestionsData questionsData = JsonUtility.FromJson<QuizData.QuestionsData>(obj.Result.text);
		//ListOutQuestions(questionsData.Questions);
		GetRandomQuestions(questionsData.Questions);
	}

	protected override void CreateQuestion()
	{
		QuizQuestionAnimal questionAnimal = Instantiate(_quizQuestionAnimalPrefab, transform.parent);
		questionAnimal.InitializeQuestion(_questionsToAsk.Dequeue(), GetAnswer);
	}

}
