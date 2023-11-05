using RinoLocalize.Common;
using RinoLocalize.RunTime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace RinoLocalize.Component
{
	public class LocalizeTMP: TextMeshProUGUI
	{
		public bool UseLocalize = true;

		[ShowIf("UseLocalize")]
		[ValueDropdown("@LocalizeDrawer.LocalizeStingIdDropDown()")]
		[ValidateInput("@LocalizeDrawer.HasLocalizeData(LocalizeStingId)", "沒有此ID的本地化資料")]
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
	}
}