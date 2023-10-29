using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace RinoLocalize.Common
{
	[Serializable]
	public class LocalizeStruct
	{
		[HideInInspector]
		public LanguageType LanguageType;
		
		[LabelText("@LanguageType.Language")]
		[ShowIf("DataType",LocalizeDataType.String)]
		public string StringValue;
		
		[LabelText("@LanguageType.Language")]
		[ShowIf("DataType",LocalizeDataType.Image),PreviewField]
		public Sprite ImageValue;

		[LabelText("@LanguageType.Language")]
		[ShowIf("DataType",LocalizeDataType.Audio)]
		public AudioClip AudioValue;

		[HideInInspector]
		public LocalizeDataType DataType;
	}
}