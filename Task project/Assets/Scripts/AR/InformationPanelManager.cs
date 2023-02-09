using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	private void MarkerScannedEventRecieved(object o, MarkerScannedEvent mse)
	{
		if (mse.type == MarkerScannedEvent.Type.Scanned)
		{
			StartCoroutine(LoadInfoAsset(mse.addressableName));
		}
		else if (mse.type == MarkerScannedEvent.Type.Cleared)
		{
			_informationShower.CloseShower();
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
}
