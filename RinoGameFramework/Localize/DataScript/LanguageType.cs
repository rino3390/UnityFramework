using RinoGameFramework.Utility.Editor.Validate;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace RinoGameFramework.Localize.DataScript
{
	[HideMonoScript]
	public class LanguageType: SerializedScriptableObject
	{
		[HideReferenceObjectPicker, OnValueChanged("ChangeListValue", IncludeChildren = true)]
		[LabelText("語言"), Required("語言類型不能為空"),UniqueList]
		[ListDrawerSettings(AlwaysAddDefaultValue = true)]
		public List<Localize.LanguageType> LanguageName;

		public event Action OnListValueChange;

		public void Reset()
		{
			LanguageName = new List<Localize.LanguageType>();
		}

	#if UNITY_EDITOR
		
		private void ChangeListValue()
		{
			OnListValueChange?.Invoke();
		}
	#endif
	}
}