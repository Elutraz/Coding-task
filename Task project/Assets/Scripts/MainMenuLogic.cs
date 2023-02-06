using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic : MonoBehaviour
{
	[SerializeField] private List<GameObject> _shownObjectsOnSplashScreen = new List<GameObject>();

	[SerializeField] private List<GameObject> _shownObjectsAfterSplashScreen = new List<GameObject>();

	[SerializeField] private float _splashScreenDuration = 3f;

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		if (!ApplicationManager.Instance.CheckMainMenuSplashScreenDone())
		{
			StartCoroutine(ShowSplashScreen());
		}
		else
		{
			ToggleScreenObjects(_shownObjectsOnSplashScreen, false);
		}
	}

	private IEnumerator ShowSplashScreen()
	{
		ToggleScreenObjects(_shownObjectsAfterSplashScreen, false);

		yield return new WaitForSeconds(_splashScreenDuration);

		ToggleScreenObjects(_shownObjectsOnSplashScreen, false);
		ToggleScreenObjects(_shownObjectsAfterSplashScreen, true);
	}

	private void ToggleScreenObjects(List<GameObject> screenGameObjects, bool active)
	{
		foreach(var gameObject in screenGameObjects)
		{
			gameObject.SetActive(active);
		}
	}
}
