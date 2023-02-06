using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public void ChangeToScene(ScriptableObjectScene scriptableObjectScene)
	{
		//Debug.Log("Scene name to load: " + scriptableObjectScene.SceneName);
		SceneManager.LoadScene(scriptableObjectScene.SceneName);
	}

	public void QuitApplication()
	{
		Application.Quit();
	}
}
