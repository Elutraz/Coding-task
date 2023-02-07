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

	protected void PanelFinish(int correctAnswers)
	{
		_onComplete.Invoke(correctAnswers);
		Destroy(gameObject);
	}
}
