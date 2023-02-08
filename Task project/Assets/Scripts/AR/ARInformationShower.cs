using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARInformationShower : MonoBehaviour
{
	public virtual void GetInformationData(ScriptableObject infoData) { }

	public virtual void CommandRecieved(InformationPanelManager.Command command) { }
}
