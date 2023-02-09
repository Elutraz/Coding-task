using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ARInformationAnimal : ARInformationShower
{
	[SerializeField] private TextMeshProUGUI _textMesh;

	[SerializeField] private Image _image;

	[SerializeField] private ScriptableObjectAnimalInfo _animalInfoData;

	private Command switchState = Command.Close;

	private void Awake()
	{
		ToggleActive(false);
	}

	public override void GetInformationData(ScriptableObject infoData)
	{
		if (infoData is not ScriptableObjectAnimalInfo animalInfoData)
		{
			return;
		}

		if (_animalInfoData != null)
		{
			ClearInfoData();
		}

		_animalInfoData = animalInfoData;

		InitializeInfoData();
	}

	private void ClearInfoData()
	{
		_animalInfoData = null;

		_textMesh.text = "";
		_textMesh.enabled = false;

		_image.sprite = null;
		_image.enabled = false;

		switchState = Command.Close;
	}

	private void InitializeInfoData()
	{
		_image.sprite = _animalInfoData.MapImage;

		ToggleActive(true);
	}

	public override void CommandRecieved(Command command)
	{
		switch (command)
		{
			case Command.MainInfo:
				ToggleMainInfo();
				break;

			case Command.ShortInfo:
				ToggleShortInfo();
				break;

			case Command.ShowMap:
				ToggleMap();
				break;

			case Command.Close:
				CloseInfoPanel();
				break;
		}

		switchState = command;
	}

	private void ToggleMainInfo()
	{
		if (_image.enabled)
		{
			_image.enabled = false;
		}

		if (!_textMesh.enabled)
		{
			_textMesh.text = _animalInfoData.AnimalInformation;
			_textMesh.enabled = true;
			return;
		}

		if (switchState == Command.MainInfo)
		{
			_textMesh.enabled = !_textMesh.enabled;
		}
		else if (switchState == Command.ShortInfo)
		{
			_textMesh.text = _animalInfoData.AnimalInformation;
		}
	}

	private void ToggleShortInfo()
	{
		if (_image.enabled)
		{
			_image.enabled = false;
		}

		if (!_textMesh.enabled)
		{
			SetShortInfoText();
			_textMesh.enabled = true;
			return;
		}

		if (switchState == Command.ShortInfo)
		{
			_textMesh.enabled = !_textMesh.enabled;
		}
		else if (switchState == Command.MainInfo)
		{
			SetShortInfoText();
		}
	}

	private void SetShortInfoText()
	{
		ScriptableObjectAnimalInfo.ShortInformation shortInfo = _animalInfoData.CompactInformation;

		_textMesh.text = "Family type animal belongs to: " + shortInfo.FamilyType + "\n";
		_textMesh.text += "Continet in which most live: " + shortInfo.Continet.ToString() + "\n";
		_textMesh.text += "Average mass of animal in kilograms: " + shortInfo.AverageBodyMassKG.ToString() + "\n";
		_textMesh.text += "Small description of an animal: " + shortInfo.Description;
	}

	private void ToggleMap()
	{
		if (_textMesh.enabled)
		{
			_textMesh.enabled = false;
		}

		_image.enabled = !_image.enabled;
	}

	private void CloseInfoPanel()
	{
		ToggleActive(false);
		ClearInfoData();
	}

	private void ToggleActive(bool active)
	{
		gameObject.SetActive(active);
	}

	
}
