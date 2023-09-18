using RinoLocalize.RunTime;
using Sirenix.OdinInspector;
using TMPro;
using Zenject;
#if UNITY_EDITOR
using GameFramework.RinoUtility.Editor;
using RinoLocalize.DataScript;
using System.Collections;
using UnityEditor;
#endif

namespace RinoLocalize.Component
{
	public class LocalizeTMP: TextMeshProUGUI
	{
		public bool UseLocalize = true;

		[ShowIf("UseLocalize")]
		[ValueDropdown("LocalizeStingIdDropDown")]
		public string LocalizeStingId;

		[Inject]
		private LocalizeManager localizeManager;

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

		public void ChangeLocalizeId(string id)
		{
			LocalizeStingId = id;
			UseLocalize = true;
			text = localizeManager.GetLocalizeString(LocalizeStingId);
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
			if(localizeDataSet == null)
			{
				return null;
			}
			return localizeDataSet.LocalizeStringDropDown();
		}
	#endif
	}
}