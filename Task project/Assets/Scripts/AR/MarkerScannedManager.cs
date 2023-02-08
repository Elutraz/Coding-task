using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;

public class MarkerScannedManager : MonoBehaviour
{
	public static MarkerScannedManager Instance;

	[SerializeField] private ARTrackedImageManager _arTrackedImageManager;

	private EventHandler<MarkerScannedEvent> _markerScannedEvent;

	[Space, Header("Testing variables")]
	public bool Sendvent = false;

	public MarkerScannedEvent.Type EventType;

	public string AddresableName = "";

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

	private void Update()
	{
		if (Sendvent)
		{
			Sendvent = false;
			SendMarkerScannedEvent(new MarkerScannedEvent(EventType, AddresableName));
		}
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
}
