using RinoGameFramework.Utility;
using RinoGameFramework.Utility.Editor;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RinoGameFramework.Localize.DataScript
{
	public class LocalizeDataSet: SerializedScriptableObject
	{
		[SerializeField,LabelText("本地化文字資料")]
		[ListDrawerSettings(HideAddButton = true, ListElementLabelName = "LanguageId", NumberOfItemsPerPage = 20,CustomRemoveElementFunction = "RemoveElement")]
		[DisableContextMenu, Searchable]
		private List<LocalizeData> LocalizeStringDatas;

		// public LanguageStringValue GetString(string Id)
		// {
		// 	var data = LocalizeStringDatas.Find(x=> x.Id == Id);
		//
		// 	if(data == null)
		// 	{
		// 		throw new MissingReferenceException($"Language{Id} 不存在");
		// 	}
		//
		// 	return data.Language;
		// }
		
	#if UNITY_EDITOR
		[SerializeField,PropertyOrder(-1)]
		[HideLabel, Title("新增本地化文字")]
		private LocalizeData Create;

		private LanguageType languageType;

		[Button, PropertyOrder(-1)]
		[PropertySpace(10, 10)]
		public void CreateLanguage()
		{
			Create.Id = GUID.NewGuid();

			if(string.IsNullOrEmpty(Create.LanguageId))
			{
				return;
			}

			LocalizeStringDatas.Add(Create);
			languageType.OnListValueChange += Create.ModifyLanguageList;
			
			SetNewCreate();
		}

		private void OnEnable()
		{
			languageType = RinoEditorUtility.FindAsset<LanguageType>();
			SetNewCreate();
			LocalizeStringDatas.ForEach(x => x.RegisterEvent(languageType));
		}

		private void OnDisable()
		{
			Create = null;
		}

		private void SetNewCreate()
		{
			Create = new LocalizeData();
			Create.RegisterEvent(languageType);
			Create.ModifyLanguageList();
		}

		public List<(string id, string root)> LanguageIdList =>
			LocalizeStringDatas.Select(languageData => (languageData.Id, ArgRoot(languageData) + languageData.LanguageId)).ToList();

		public IEnumerable LanguageDropDown()
		{
			return LocalizeStringDatas.Select(languageData => new ValueDropdownItem(ArgRoot(languageData) + languageData.LanguageId, languageData.Id));
		}

		private static string ArgRoot(LocalizeData localize)
		{
			return string.IsNullOrEmpty(localize.Root) ? "" : localize.Root + "/";
		}

		private void RemoveElement(object obj)
		{
			var data = (LocalizeData)obj;
			languageType.OnListValueChange -= data.ModifyLanguageList;
			LocalizeStringDatas.Remove(data);
		}
	#endif
	}
}