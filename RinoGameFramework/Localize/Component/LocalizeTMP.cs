using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEditor;
using UnityEngine;
using VContainer;

namespace RinoGameFramework.Localize
{
	public class LocalizeTMP: TextMeshProUGUI
	{
		public bool UseLocalize = true;

		[ShowIf(nameof(UseLocalize))]
		public string LocalizeStingId;

		[Inject]
		private LocalizeManager localizeManager;

		protected override void Awake()
		{
			base.Awake();

			if(EditorApplication.isPlaying)
			{
				localizeManager.OnLanguageChange += OnLanguageChange;
			}
		}

		private void OnLanguageChange(string obj)
		{
			Debug.Log($"Select");
		}
	}
}