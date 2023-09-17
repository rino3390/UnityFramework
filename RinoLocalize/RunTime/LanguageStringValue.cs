using Sirenix.OdinInspector;
using System;

namespace RinoLocalize.RunTime
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