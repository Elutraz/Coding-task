using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class QuizQuestionPanel : MonoBehaviour
{
	[SerializeField] protected string _addressableName = "";

	private Action<int> _onComplete;

	protected int _numberOfQuestionsToAsk = 0;

	protected int _correctAnsweredQuestions = 0;

	protected Queue<QuizData.Question> _questionsToAsk = new Queue<QuizData.Question>();

	public virtual void InitializePanel(int amountOfQuestions, Action<int> onComplete)
	{
		_numberOfQuestionsToAsk = amountOfQuestions;
		_correctAnsweredQuestions = 0;

		_onComplete += onComplete;
		StartCoroutine(LoadAsset());
	}

	protected IEnumerator LoadAsset()
	{
		AsyncOperationHandle<TextAsset> textAssetHandle = Addressables.LoadAssetAsync<TextAsset>(_addressableName);
		yield return textAssetHandle;

		if(textAssetHandle.Status == AsyncOperationStatus.Failed)
		{
			Addressables.Release(textAssetHandle);
			PanelFinish(-1);
			yield break;
		}

		textAssetHandle.Completed += LoadingAssetCompleted;

		Addressables.Release(textAssetHandle);
	}

	protected abstract void LoadingAssetCompleted(AsyncOperationHandle<TextAsset> obj);

	protected virtual void GetRandomQuestions(List<QuizData.Question> questions)
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

	protected void PanelFinish(int correctAnswers)
	{
		_onComplete.Invoke(correctAnswers);
		Destroy(gameObject);
	}

	protected abstract void CreateQuestion();

	protected virtual void GetAnswer(bool correctAnswer)
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

	protected void ListOutQuestions(List<QuizData.Question> questions)
	{
		foreach (var question in questions)
		{
			Debug.Log("Question number: " + question.QuestionNumber + " Question text: " + question.QuestionText + " Correct answer: " + question.CorrectAnswer);
		}
	}
}
