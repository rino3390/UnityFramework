using Sirenix.OdinInspector;
using System;

namespace RinoGameFramework.Localize
{
	[Serializable]
	public struct LanguageStringValue
	{
		[LabelText("繁體中文")]
		public string zh_Language;
		[LabelText("英文")]
		public string en_Language;
	}
}