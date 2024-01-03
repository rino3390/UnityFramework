using GameFramework.GameManagerBase.EditorBase;
using GameFramework.GameManagerBase.Extension;
using GameFramework.RinoUtility.Attribute;
using GameFramework.RinoUtility.Editor;
using RinoLocalize.Common;
using RinoLocalize.DataScript;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GUID = GameFramework.RinoUtility.Misc.GUID;

namespace RinoLocalize.Editor
{
	public class LocalizeManagerEditorWindow: GameEditorMenu
	{
		[HorizontalGroup("Editor", 0.2f, MarginRight = 10), PropertyOrder(-3), PropertySpace(10)]
		[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
		[SerializeField]
		public LanguageList languageList;

		[Space(10), PropertyOrder(2), ShowIf("localizeDataType", LocalizeDataType.Audio)]
		[HorizontalGroup("Editor")]
		[VerticalGroup("Editor/LocalizeData")]
		[SerializeField, LabelText("本地化音效資料")]
		[ListDrawerSettings(HideAddButton = true, ListElementLabelName = "DisplayName", NumberOfItemsPerPage = 20, ShowFoldout = false)]
		[DisableContextMenu(true, true), Searchable, LocalizeTable, UniqueList("含有相同路徑的重複ID")]
		public List<LocalizeData> LocalizeAudioDatas;

		[Space(10), PropertyOrder(2), ShowIf("localizeDataType", LocalizeDataType.Image)]
		[HorizontalGroup("Editor")]
		[VerticalGroup("Editor/LocalizeData")]
		[SerializeField, LabelText("本地化圖片資料")]
		[ListDrawerSettings(HideAddButton = true, ListElementLabelName = "DisplayName", NumberOfItemsPerPage = 20, ShowFoldout = false)]
		[DisableContextMenu(true, true), Searchable, LocalizeTable, UniqueList("含有相同路徑的重複ID")]
		public List<LocalizeData> LocalizeImageDatas;

		[Space(10), PropertyOrder(2), ShowIf("localizeDataType", LocalizeDataType.String)]
		[HorizontalGroup("Editor")]
		[VerticalGroup("Editor/LocalizeData")]
		[SerializeField, LabelText("本地化文字資料")]
		[ListDrawerSettings(HideAddButton = true, ListElementLabelName = "DisplayName", NumberOfItemsPerPage = 20, ShowFoldout = false)]
		[DisableContextMenu(true, true), Searchable, LocalizeTable, UniqueList("含有相同路徑的重複ID")]
		public List<LocalizeData> LocalizeStringDatas;

		[SerializeField, HideInInspector]
		private LocalizeDataSet localizeDataSet;

		private LocalizeDataType localizeDataType;

		[HorizontalGroup("Editor")]
		[VerticalGroup("Editor/LocalizeData")]
		[SerializeField, PropertyOrder(-1), BoxGroup("Editor/LocalizeData/Create")]
		[HideLabel]
		[ValidateInput("@LocalizeData?.CheckId", "含有相同路徑的重複ID")]
		private LocalizeData LocalizeData;

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = SetTree(width: 0).AddSelfMenu(this);
			return tree;
		}

		[Button("文字資料", Icon = SdfIconType.Fonts), PropertyOrder(-2), PropertySpace(10, 10)]
		[HorizontalGroup("Editor")]
		[VerticalGroup("Editor/LocalizeData")]
		[HorizontalGroup("Editor/LocalizeData/Button", MarginLeft = 0.02f)]
		public void StringPage()
		{
			localizeDataType = LocalizeDataType.String;
			ModifyLanguage();
		}

		[Button("圖片資料", Icon = SdfIconType.ImageFill), PropertyOrder(-2), PropertySpace(10, 10)]
		[HorizontalGroup("Editor")]
		[VerticalGroup("Editor/LocalizeData")]
		[HorizontalGroup("Editor/LocalizeData/Button")]
		public void ImagePage()
		{
			localizeDataType = LocalizeDataType.Image;
			ModifyLanguage();
		}

		[Button("音效資料", Icon = SdfIconType.MusicNoteBeamed), PropertyOrder(-2), PropertySpace(10, 10)]
		[HorizontalGroup("Editor")]
		[VerticalGroup("Editor/LocalizeData")]
		[HorizontalGroup("Editor/LocalizeData/Button", MarginRight = 0.02f)]
		public void AudioPage()
		{
			localizeDataType = LocalizeDataType.Audio;
			ModifyLanguage();
		}

		[HorizontalGroup("Editor")]
		[VerticalGroup("Editor/LocalizeData")]
		[Button("Create"), PropertyOrder(-1), BoxGroup("Editor/LocalizeData/Create"), GUIColor(0.67f, 1f, 0.65f)]
		[PropertySpace(10, 10)]
		public void CreateLocalizeString()
		{
			if(!LocalizeData.CheckId)
			{
				return;
			}

			LocalizeData.Id = GUID.NewGuid();

			if(string.IsNullOrEmpty(LocalizeData.LanguageId))
			{
				return;
			}

			switch(localizeDataType)
			{
				case LocalizeDataType.String:
					localizeDataSet.AddStringData(LocalizeData);
					break;
				case LocalizeDataType.Image:
					localizeDataSet.AddImageData(LocalizeData);
					break;
				case LocalizeDataType.Audio:
					localizeDataSet.AddAudioData(LocalizeData);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			SetNewLocalizeData();
		}

		private void ModifyLanguage()
		{
			LocalizeData.LocalizeValue.Clear();

			foreach(var languageType in languageList.LanguageName)
			{
				LocalizeData.LocalizeValue.Add(new LocalizeStruct { LanguageType = languageType, DataType = localizeDataType });
			}
		}

		private void SetNewLocalizeData()
		{
			LocalizeData = new LocalizeData
			{
				DataType = localizeDataType,
			};

			foreach(var languageType in languageList.LanguageName)
			{
				LocalizeData.LocalizeValue.Add(new LocalizeStruct { LanguageType = languageType, DataType = localizeDataType });
			}
		}

		protected override void OnEnable()
		{
			localizeDataSet = RinoEditorUtility.FindAsset<LocalizeDataSet>();

			LocalizeStringDatas = localizeDataSet.LocalizeStringDatas;
			LocalizeImageDatas = localizeDataSet.LocalizeImageDatas;
			LocalizeAudioDatas = localizeDataSet.LocalizeAudioDatas;

			languageList = RinoEditorUtility.FindAsset<LanguageList>();
			languageList.OnLanguageChange += (_, _) => ModifyLanguage();
			languageList.OnLanguageAdd += _ => ModifyLanguage();
			languageList.OnLanguageInsert += (_, _) => ModifyLanguage();
			languageList.OnLanguageRemove += _ => ModifyLanguage();

			SetNewLocalizeData();
		}

		private void OnLostFocus()
		{
			RinoEditorUtility.SaveSOData(new SerializedObject(localizeDataSet));
		}
	}
}