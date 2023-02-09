using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ARInformationShower : MonoBehaviour
{
	public virtual void GetInformationData(ScriptableObject infoData) { }

	public virtual void CommandRecieved(Command command) { }

	public void CloseShower()
	{
		CommandRecieved(Command.Close);
	}

	[Serializable]
	public enum Command
	{
		Close,
		MainInfo,
		ShortInfo,
		ShowMap,
	}
}
