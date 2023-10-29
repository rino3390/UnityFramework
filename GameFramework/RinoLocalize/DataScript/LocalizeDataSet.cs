using GameFramework.RinoUtility.Attribute;
using GameFramework.RinoUtility.Editor;
using RinoLocalize.Common;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace RinoLocalize.DataScript
{
	public class LocalizeDataSet: SerializedScriptableObject
	{
		[SerializeField, LabelText("本地化文字資料"), TabGroup("文字資料")]
		[ListDrawerSettings(HideAddButton = true, ListElementLabelName = "DisplayName", NumberOfItemsPerPage = 20, ShowFoldout = false)]
		[DisableContextMenu(true, true), Searchable, LocalizeTable, UniqueList("含有相同路徑的重複ID")]
		public List<LocalizeData> LocalizeStringDatas;

		[SerializeField, LabelText("本地化圖片資料"), TabGroup("圖片資料")]
		[ListDrawerSettings(HideAddButton = true, ListElementLabelName = "DisplayName", NumberOfItemsPerPage = 20, ShowFoldout = false)]
		[DisableContextMenu(true, true), Searchable, LocalizeTable, UniqueList("含有相同路徑的重複ID")]
		public List<LocalizeData> LocalizeImageDatas;

		[SerializeField, LabelText("本地化音效資料"), TabGroup("音效資料")]
		[ListDrawerSettings(HideAddButton = true, ListElementLabelName = "DisplayName", NumberOfItemsPerPage = 20, ShowFoldout = false)]
		[DisableContextMenu(true, true), Searchable, LocalizeTable, UniqueList("含有相同路徑的重複ID")]
		public List<LocalizeData> LocalizeAudioDatas;

		public void AddStringData(LocalizeData data)
		{
			LocalizeStringDatas.Add(data);
		}

		public void AddImageData(LocalizeData data)
		{
			LocalizeImageDatas.Add(data);
		}

		public void AddAudioData(LocalizeData data)
		{
			LocalizeAudioDatas.Add(data);
		}

		public List<LocalizeString> GetString(string Id)
		{
			var data = LocalizeStringDatas.Find(x => x.Id == Id);

			if(data == null)
			{
				throw new MissingReferenceException($"{Id} 字串資料不存在");
			}

			var value = data.LocalizeValue.Select(x => new LocalizeString
							{
								Language = x.LanguageType.Language,
								Value =  x.StringValue
							})
							.ToList();

			return value;
		}

		public List<LocalizeImage> GetImage(string Id)
		{
			var data = LocalizeImageDatas.Find(x => x.Id == Id);

			if(data == null)
			{
				throw new MissingReferenceException($"{Id} 圖片資料不存在");
			}

			var value = data.LocalizeValue.Select(x => new LocalizeImage
							{
								Language = x.LanguageType.Language,
								Value = x.ImageValue
							})
							.ToList();

			return value;
		}

		public List<LocalizeAudio> GetAudio(string Id)
		{
			var data = LocalizeAudioDatas.Find(x => x.Id == Id);

			if(data == null)
			{
				throw new MissingReferenceException($"{Id} 音效資料不存在");
			}

			var value = data.LocalizeValue.Select(x => new LocalizeAudio
							{
								Language = x.LanguageType.Language,
								Value = x.AudioValue
							})
							.ToList();

			return value;
		}

		public static string RootId(LocalizeData localize)
		{
			return ( string.IsNullOrEmpty(localize.Root) ? "" : localize.Root + "/" ) + localize.LanguageId;
		}

	#if UNITY_EDITOR

		private LanguageList languageList;

		private void OnEnable()
		{
			languageList = RinoEditorUtility.FindAsset<LanguageList>();

			languageList.OnLanguageAdd += OnLanguageAdd;
			languageList.OnLanguageChange += OnLanguageChange;
			languageList.OnLanguageRemove += OnLanguageRemove;
			languageList.OnLanguageInsert += OnLanguageInsert;
		}

		public List<(string id, string root)> StringDataIdList =>
			LocalizeStringDatas.Select(languageData => (languageData.Id, RootId(languageData))).ToList();

		public List<(string id, string root)> ImageDataIdList =>
			LocalizeImageDatas.Select(languageData => (languageData.Id, RootId(languageData))).ToList();

		public List<(string id, string root)> AudioDataIdList =>
			LocalizeAudioDatas.Select(languageData => (languageData.Id, RootId(languageData))).ToList();
		
		public IEnumerable LocalizeStringDropDown()
		{
			return LocalizeStringDatas.Select(languageData => new ValueDropdownItem(RootId(languageData), languageData.Id));
		}

		public IEnumerable LocalizeImageDropDown()
		{
			return LocalizeImageDatas.Select(languageData => new ValueDropdownItem(RootId(languageData), languageData.Id));
		}

		public IEnumerable LocalizeAudioDropDown()
		{
			return LocalizeAudioDatas.Select(languageData => new ValueDropdownItem(RootId(languageData), languageData.Id));
		}

	#region LanguageChangeEvent
		private void OnLanguageChange(string id, string language)
		{
			LocalizeStringDatas.ForEach(x => x.LocalizeValue.Find(y => y.LanguageType.Id == id).LanguageType.Language = language);
			LocalizeImageDatas.ForEach(x => x.LocalizeValue.Find(y => y.LanguageType.Id == id).LanguageType.Language = language);
			LocalizeAudioDatas.ForEach(x => x.LocalizeValue.Find(y => y.LanguageType.Id == id).LanguageType.Language = language);
		}

		private void OnLanguageAdd(LanguageType addLanguage)
		{
			LocalizeStringDatas.ForEach(x => x.LocalizeValue.Add(new LocalizeStruct { LanguageType = addLanguage }));
			LocalizeImageDatas.ForEach(x => x.LocalizeValue.Add(new LocalizeStruct { LanguageType = addLanguage }));
			LocalizeAudioDatas.ForEach(x => x.LocalizeValue.Add(new LocalizeStruct { LanguageType = addLanguage }));
		}

		private void OnLanguageRemove(string language)
		{
			LocalizeStringDatas.ForEach(x => x.LocalizeValue.RemoveAll(y => y.LanguageType.Language == language));
			LocalizeImageDatas.ForEach(x => x.LocalizeValue.RemoveAll(y => y.LanguageType.Language == language));
			LocalizeAudioDatas.ForEach(x => x.LocalizeValue.RemoveAll(y => y.LanguageType.Language == language));
		}

		private void OnLanguageInsert(int i, LanguageType languageType)
		{
			LocalizeStringDatas.ForEach(x => { x.LocalizeValue.Insert(i, new LocalizeStruct { LanguageType = languageType }); });
			LocalizeImageDatas.ForEach(x => { x.LocalizeValue.Insert(i, new LocalizeStruct { LanguageType = languageType }); });
			LocalizeAudioDatas.ForEach(x => { x.LocalizeValue.Insert(i, new LocalizeStruct { LanguageType = languageType }); });
		}
	#endregion

	#endif
	}
}