using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace RinoGameFramework.Localize
{
	[Serializable]
	public class LanguageString
	{
		[SerializeField]
		[ToggleLeft, LabelText("綁定LanguageId")]
		[HorizontalGroup("toggle")]
		public bool useLanguageId;

		[SerializeField, HideLabel]
		[ShowIf("useLanguageId")]
		[HorizontalGroup("toggle")]
		[ValueDropdown("@OdinDropDown.Languages()")]
		public string LanguageId;

		[SerializeField]
		[HideLabel]
		[ShowIf("@!useLanguageId")]
		public LanguageStringValue languageValue;
	}
}