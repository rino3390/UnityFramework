using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RinoGameFramework.Localize
{
	[Serializable]
	public class LocalizeStringStruct
	{
		[HideInInspector]
		public LanguageType LanguageType;
		
		[LabelText("@LanguageType.Language")]
		public string StringValue;
	}
}