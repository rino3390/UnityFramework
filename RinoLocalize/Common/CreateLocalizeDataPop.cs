#if UNITY_EDITOR
using GameFramework.RinoUtility.Editor;
using GameFramework.RinoUtility.Misc;
using RinoLocalize.DataScript;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using UnityEngine;

namespace RinoLocalize.Common
{
	[Serializable,HideReferenceObjectPicker]
	public class OpenCreateLocalizePopWindow
	{
		private readonly LocalizeDataType localizeDataType;
		private readonly Action<string> changeLocalizeId;

		public OpenCreateLocalizePopWindow(LocalizeDataType localizeDataType,Action<string> changeLocalizeId)
		{
			this.localizeDataType = localizeDataType;
			this.changeLocalizeId = changeLocalizeId;
		}
		
		[Button, HorizontalGroup("row1")]
		private void CreateNewLocalizeData()
		{
			var btnRect = GUIHelper.GetCurrentLayoutRect();
			var dataPop = new CreateLocalizeDataPop();
			var window = OdinEditorWindow.InspectObjectInDropDown(dataPop, btnRect, btnRect.width);
			dataPop.OpenWindow(localizeDataType);
			dataPop.OnCreate += () =>
			{
				changeLocalizeId(dataPop.LocalizeData.Id);
				window.Close();
			};
		}
	}
	
	[Serializable]
	public class CreateLocalizeDataPop
	{
		[HideLabel,SerializeField]
		[ValidateInput("@LocalizeData?.CheckId", "含有相同路徑的重複ID")]
		public LocalizeData LocalizeData;

		private LocalizeDataType dataType;
		private LocalizeDataSet localizeDataSet;
		
		public event Action OnCreate;

		public void OpenWindow(LocalizeDataType localizeDataType)
		{
			var languageList = RinoEditorUtility.FindAsset<LanguageList>();
			localizeDataSet = RinoEditorUtility.FindAsset<LocalizeDataSet>();
			dataType = localizeDataType;
			LocalizeData = new LocalizeData
			{
				DataType = dataType,
			};

			foreach(var languageType in languageList.LanguageName)
			{
				LocalizeData.LocalizeValue.Add(new LocalizeStruct { LanguageType = languageType, DataType = dataType });
			}
		}
		
		[Button("Create"), GUIColor(0.67f, 1f, 0.65f)]	
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

			switch(dataType)
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
			OnCreate?.Invoke();
		}
	}
}
#endif