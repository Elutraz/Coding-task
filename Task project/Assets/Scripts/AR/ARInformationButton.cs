using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ARInformationButton : MonoBehaviour
{
	[SerializeField] private InformationPanelManager _informationPanelManager;

	[SerializeField] private InformationPanelManager.Command _infoPanelCommand;

	public void ButtonClicked()
	{
		_informationPanelManager?.CommandActivated(_infoPanelCommand);
	}
}
