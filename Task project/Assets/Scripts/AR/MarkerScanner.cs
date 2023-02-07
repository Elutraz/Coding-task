using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MarkerScanner : MonoBehaviour
{
	[SerializeField] private ARTrackedImageManager _arTrackedImageManager;

	private void OnEnable()
	{
		_arTrackedImageManager.trackedImagesChanged += OnImageChanged;
	}

	private void OnDisable()
	{
		_arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
	}

	private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
	{
		foreach(var scannedImage in eventArgs.added)
		{
			Debug.Log(scannedImage.name);
		}
	}
}
