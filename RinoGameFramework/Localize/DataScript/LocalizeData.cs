using RinoGameFramework.Utility.Editor;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RinoGameFramework.Localize.DataScript
{
	[Serializable]
	public class LocalizeData
	{
		[HideInInspector]
		public string Id;

		[LabelText("語言Id")]
		[InfoBox("含有相同路徑的重複Id", InfoMessageType.Warning, "CheckId")]
		public string LanguageId;

		[LabelText("分類 / 路徑")]
		public string Root;

		[LabelText("本地化文字")]
		public List<LocalizeStringStruct> LocalizeString = new List<LocalizeStringStruct>();

		private LanguageType _languageType;

	#if UNITY_EDITOR
		public void RegisterEvent(LanguageType languageType)
		{
			_languageType = languageType;
			_languageType.OnListValueChange += ModifyLanguageList;
		}

		public void ModifyLanguageList()
		{
			foreach(var languageType in _languageType.LanguageName)
			{
				var changedLanguage =
					LocalizeString.FirstOrDefault(y => languageType.Id == y.LanguageType.Id && languageType.Language != y.LanguageType.Language);

				if(changedLanguage != null)
				{
					changedLanguage.LanguageType.Language = languageType.Language;
					return;
				}

				if(LocalizeString.All(y => y.LanguageType.Id != languageType.Id))
				{
					LocalizeString.Add(new LocalizeStringStruct { LanguageType = languageType });
				}
			}

			LocalizeString.RemoveAll(x => _languageType.LanguageName.All(y => y.Id != x.LanguageType.Id));
		}

		private bool CheckId
		{
			get
			{
				var data = RinoEditorUtility.FindAsset<LocalizeDataSet>();
				var root = string.IsNullOrEmpty(Root) ? "" : Root + "/";

				return data != null && data.LanguageIdList.Any(x => x.root == root + LanguageId && x.id != Id);
			}
		}
	#endif
	}
}