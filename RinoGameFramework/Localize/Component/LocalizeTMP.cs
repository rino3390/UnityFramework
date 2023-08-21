using Sirenix.OdinInspector;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using VContainer;

namespace RinoGameFramework.Localize
{
	public class LocalizeTMP: TextMeshProUGUI
	{
		public bool UseLocalize = true;

		[ShowIf("UseLocalize")]
		public string LocalizeStingId;

		[Inject]
		private LocalizeManager localizeManager;

		protected override void Awake()
		{
			base.Awake();

		#if UNITY_EDITOR
			if (EditorApplication.isPlaying)
			{
				localizeManager.OnLanguageChange += OnLanguageChange;
			}
		#endif
		}

		private void OnLanguageChange(string obj)
		{
			Debug.Log($"Select");
		}
	}
}