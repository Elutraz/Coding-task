using System;

public class MarkerScannedEvent : EventArgs
{
	public Type type;
	public string addressableName;

	public MarkerScannedEvent(Type inType, string inAddressableName = "")
	{
		type = inType;
		addressableName = inAddressableName;
	}

	public enum Type
	{
		Scanned,
		Cleared,
	}
}
