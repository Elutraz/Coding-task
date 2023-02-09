using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public class LanguageSelection : MonoBehaviour
{
	[SerializeField] private TMP_Dropdown _dropdownMenu;

	private string _languagePersistenceName = "LanguageSelection";

	private void Awake()
	{
		_dropdownMenu.onValueChanged.AddListener(delegate { OnDropdownValueChanged(); });
		InitializeDropdown();
	}

	private void OnDestroy()
	{
		_dropdownMenu.onValueChanged.RemoveListener(delegate { OnDropdownValueChanged(); });
	}

	private void OnDropdownValueChanged()
	{
		PlayerPrefs.SetInt(_languagePersistenceName, _dropdownMenu.value);
	}

	private void InitializeDropdown()
	{
		foreach(var language in Enum.GetNames(typeof(Language)))
		{
			_dropdownMenu.options.Add(new TMP_Dropdown.OptionData(language.ToString()));
		}

		_dropdownMenu.value = PlayerPrefs.GetInt(_languagePersistenceName);
	}

	[Serializable]
	public enum Language
	{
		English,
		German,
		Spanish,
		French,
		Chinese
	}
}
