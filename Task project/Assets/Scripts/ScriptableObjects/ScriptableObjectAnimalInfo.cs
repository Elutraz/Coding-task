using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AnimalInfo", menuName = "ScriptableObject/AnimalInfo")]

public class ScriptableObjectAnimalInfo : ScriptableObject
{
	public string AnimalInformation = "";
	public ShortInformation CompactInformation = new ShortInformation();
	public Texture2D MapImage;
   
	[Serializable]
	public class ShortInformation
	{
		public string FamilyType = "";
		public Continents Continet;
		public float AverageBodyMassKG = 0f;
		public string Description = "";
	}
}

public enum Continents
{
	Africa,
	Asia,
	Europe,
	North_America,
	South_America,
	Antarctica,
	Australia
}
