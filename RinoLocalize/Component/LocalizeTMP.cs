using RinoLocalize.RunTime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace RinoLocalize.Component
{
	public class LocalizeTMP: TextMeshProUGUI
	{
		public bool UseLocalize = true;

		[ShowIf("UseLocalize")]
		public string LocalizeStingId;

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