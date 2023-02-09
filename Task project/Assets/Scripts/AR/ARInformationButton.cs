using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ARInformationButton : MonoBehaviour
{
	[SerializeField] private ARInformationShower _informationShower;

	[SerializeField] private ARInformationShower.Command _infoPanelCommand;

	public void ButtonClicked()
	{
		_informationShower?.CommandRecieved(_infoPanelCommand);
	}
}
