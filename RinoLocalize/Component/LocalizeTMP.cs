using ModestTree.Util;
using RinoLocalize.Common;
using RinoLocalize.RunTime;
using Sirenix.OdinInspector;
using TMPro;
using Zenject;
using RinoLocalize.DataScript;
using System;
using System.Collections;
using System.Linq;
#if UNITY_EDITOR
using GameFramework.RinoUtility.Editor;
using UnityEditor;
#endif

namespace RinoLocalize.Component
{
	public class LocalizeTMP: TextMeshProUGUI
	{
		public bool UseLocalize = true;

		[ShowIf("UseLocalize")]
		[ValueDropdown("LocalizeStingIdDropDown"), ValidateInput("HasLocalizeData", "沒有此ID的本地化資料")]
		public string LocalizeStingId;

		[HideLabel, ShowInInspector]
		private OpenCreateLocalizePopWindow openPop;
		
		[Inject]
		private LocalizeManager localizeManager;

		public LocalizeTMP()
		{
			openPop = new OpenCreateLocalizePopWindow(LocalizeDataType.String, ChangeLocalizeId);
		}

		protected override void Awake()
		{
			base.Awake();

		#if UNITY_EDITOR
			if(EditorApplication.isPlaying)
			{
				localizeManager.OnLanguageChange += OnLanguageChange;
			}
		#endif
		}

		public void ChangeLocalizeText(string id)
		{
			ChangeLocalizeId(id);
			text = localizeManager.GetLocalizeString(LocalizeStingId);
		}

		private void ChangeLocalizeId(string id)
		{
			LocalizeStingId = id;
			UseLocalize = true;
		}

		private void OnLanguageChange(string language)
		{
			if(!UseLocalize)
			{
				return;
			}

			text = localizeManager.GetLocalizeString(LocalizeStingId);
		}

	#if UNITY_EDITOR
		private IEnumerable LocalizeStingIdDropDown()
		{
			var localizeDataSet = RinoEditorUtility.FindAsset<LocalizeDataSet>();
			return localizeDataSet == null ? null : localizeDataSet.LocalizeStringDropDown();
		}
		private bool HasLocalizeData()
		{
			var localizeDataSet = RinoEditorUtility.FindAsset<LocalizeDataSet>();
			return localizeDataSet.LocalizeStringDatas.Any(x=> x.Id == LocalizeStingId);
		}
	#endif
	}
}