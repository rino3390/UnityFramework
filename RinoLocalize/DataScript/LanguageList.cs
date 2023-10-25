using GameFramework.RinoUtility.Attribute;
using RinoLocalize.Common;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace RinoLocalize.DataScript
{
	[HideMonoScript]
	public class LanguageList: SerializedScriptableObject
	{
		[HideReferenceObjectPicker, OnValueChanged("ChangeListValue", IncludeChildren = true), OnCollectionChanged("AfterChange")]
		[LabelText("語言"), UniqueList]
		[ListDrawerSettings(AlwaysAddDefaultValue = true)]
		public List<LanguageType> LanguageName;
		
		[HideLabel,Header("預設語言")]
		[ValueDropdown("LanguageNameDropDown")]
		public string DefaultLanguage;

		public void Reset()
		{
			LanguageName = new List<LanguageType>();
		}

	#if UNITY_EDITOR

		public event Action<int, LanguageType> OnLanguageInsert;
		public event Action<LanguageType> OnLanguageAdd;
		public event Action<string, string> OnLanguageChange;
		public event Action<string> OnLanguageRemove;

		private bool isAddOrRemove;
		private List<string> tempList;

		private void OnEnable()
		{
			tempList = LanguageName.Select(x => x.Language).ToList();
		}

		private void ChangeListValue()
		{
			if(isAddOrRemove)
			{
				isAddOrRemove = false;
				return;
			}

			var differentItems = LanguageName.FirstOrDefault(x => !tempList.Contains(x.Language));

			if(differentItems == null) return;

			{
				OnLanguageChange?.Invoke(differentItems.Id, differentItems.Language);
				tempList = LanguageName.Select(x => x.Language).ToList();
			}
		}

		private void AfterChange(CollectionChangeInfo info)
		{
			isAddOrRemove = true;

			var currentLanguageList = LanguageName.Select(language => language.Language).ToList();

			if(info.ChangeType == CollectionChangeType.Add)
			{
				var value = (LanguageType)info.Value;
				OnLanguageAdd?.Invoke(value);
				tempList = currentLanguageList;
			}
			else if(info.ChangeType == CollectionChangeType.Insert)
			{
				var value = (LanguageType)info.Value;
				OnLanguageInsert?.Invoke(info.Index, value);
				tempList = currentLanguageList;
			}
			else if(info.ChangeType == CollectionChangeType.RemoveIndex)
			{
				var value = tempList.FirstOrDefault(x => !currentLanguageList.Contains(x));

				if(value == null) return;

				OnLanguageRemove?.Invoke(value);
				tempList = currentLanguageList;
			}
		}
		
		public IEnumerable LanguageNameDropDown()
		{
			return LanguageName.Select(x => new ValueDropdownItem(x.Language, x.Id));
		}
	#endif
	}
}