using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
	public static ApplicationManager Instance = null;

	private bool _mainMenuSplashScreenDone = false;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public bool CheckMainMenuSplashScreenDone()
	{
		if (_mainMenuSplashScreenDone)
		{
			return _mainMenuSplashScreenDone;
		}

		_mainMenuSplashScreenDone = true;
		return false;
	}
}
