using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using System.Collections.Generic;

public class MarkerScannedManager : MonoBehaviour
{
	public static MarkerScannedManager Instance;

	[SerializeField] private ARTrackedImageManager _arTrackedImageManager;

	private EventHandler<MarkerScannedEvent> _markerScannedEvent;

	[Space, Header("Dummy testing")]
	[SerializeField] List<KeyCodeData> _keyCodeData = new List<KeyCodeData>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnEnable()
	{
		_arTrackedImageManager.trackedImagesChanged += OnImageChanged;
	}

	private void OnDisable()
	{
		_arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
	}

	public void AddMarkerScannedEventListener(EventHandler<MarkerScannedEvent> listener)
	{
		_markerScannedEvent += listener;
	}

	public void RemoveMarkerScannedEventListener(EventHandler<MarkerScannedEvent> listener)
	{
		_markerScannedEvent -= listener;
	}

	private void SendMarkerScannedEvent(MarkerScannedEvent mse)
	{
		_markerScannedEvent?.Invoke(this, mse);
	}

	private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
	{
		foreach(var scannedImage in eventArgs.added)
		{
			Debug.Log("Added: " + scannedImage.name);
		}

		foreach (var scannedImage in eventArgs.removed)
		{
			Debug.Log("Removed: " + scannedImage.name);
		}
	}

	// Dummy testing par cause scanning marker doesnt work at this moment

	private void Update()
	{
		CheckKeyCodePressed();
	}

	private void CheckKeyCodePressed()
	{
		foreach (var keyCodeData in _keyCodeData)
		{
			if (Input.GetKeyDown(keyCodeData.KeyCode))
			{
				SendMarkerScannedEvent(new MarkerScannedEvent(MarkerScannedEvent.Type.Scanned, keyCodeData.AddressableName));
			}
		}
	}

	[Serializable]
	public class KeyCodeData
	{
		public KeyCode KeyCode;
		public string AddressableName = "";
	}

}
