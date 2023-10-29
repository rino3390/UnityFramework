using RinoLocalize.Common;
using RinoLocalize.DataScript;
using RinoLocalize.RunTime;
using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;
#if UNITY_EDITOR
using GameFramework.RinoUtility.Editor;
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

			if(Application.isPlaying)
			{
				localizeManager.OnLanguageChange += OnLanguageChange;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			if(Application.isPlaying)
			{
				SetLocalizeString();
			}
		}

		public void ChangeLocalizeText(string id)
		{
			ChangeLocalizeId(id);
			SetLocalizeString();
		}

		private void ChangeLocalizeId(string id)
		{
			LocalizeStingId = id;
			UseLocalize = true;
		}

		private void SetLocalizeString()
		{
			text = localizeManager.GetLocalizeString(LocalizeStingId);
		}

		private void OnLanguageChange(string language)
		{
			if(!UseLocalize) return;

			SetLocalizeString();
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
			return localizeDataSet.LocalizeStringDatas.Any(x => x.Id == LocalizeStingId);
		}
	#endif
	}
}