using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;


public class QuizQuestionPanelPlanets : QuizQuestionPanel
{
	protected override void LoadingAssetCompleted(AsyncOperationHandle<TextAsset> obj)
	{
		Debug.Log("laoding assets complete plaentes");
	}
}
