using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement.AsyncOperations;


public class InformationPanelManager : MonoBehaviour
{
	[SerializeField] ARInformationShower _informationShower;

	private void Start()
	{
		MarkerScannedManager.Instance.AddMarkerScannedEventListener(MarkerScannedEventRecieved);
	}

	private void OnDisable()
	{
		MarkerScannedManager.Instance.RemoveMarkerScannedEventListener(MarkerScannedEventRecieved);
	}

	public void CommandActivated(Command command)
	{
		_informationShower.CommandRecieved(command);
	}

	private void MarkerScannedEventRecieved(object o, MarkerScannedEvent mse)
	{
		if (mse.type == MarkerScannedEvent.Type.Scanned)
		{
			StartCoroutine(LoadInfoAsset(mse.addressableName));
		}
		else if (mse.type == MarkerScannedEvent.Type.Cleared)
		{
			CommandActivated(Command.Close);
		}
	}

	protected IEnumerator LoadInfoAsset(string addresableName)
	{
		AsyncOperationHandle<ScriptableObject> scriptableObjectAssetHandle = Addressables.LoadAssetAsync<ScriptableObject>(addresableName);
		yield return scriptableObjectAssetHandle;

		if (scriptableObjectAssetHandle.Status == AsyncOperationStatus.Failed)
		{
			Addressables.Release(scriptableObjectAssetHandle);
			yield break;
		}

		_informationShower.GetInformationData(scriptableObjectAssetHandle.Result);

		//Addressables.Release(scriptableObjectAssetHandle);
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
