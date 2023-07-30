using RinoGameFramework.Localize;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RinoGameFramework.GameManager.DataScript.Language
{
	[Serializable]
	public class LanguageData
	{
		[HideInInspector]
		public string Id;

		[LabelText("語言Id")]
		[InfoBox("含有相同路徑的重複Id",InfoMessageType.Warning,"CheckId")]
		public string LanguageId;

		[LabelText("分類 / 路徑")]
		public string Root;

		[HideLabel]
		public LanguageStringValue Language;

	#if UNITY_EDITOR
		private bool CheckId
		{
			get
			{
				var data = AssetDatabase.FindAssets($"t:LanguageDataOverview").Select(guid => AssetDatabase.LoadAssetAtPath<LanguageDataOverview>(AssetDatabase.GUIDToAssetPath(guid))).FirstOrDefault();
				var root = string.IsNullOrEmpty(Root)? "" : Root+"/";

				return data != null && data.LanguageIdList.Any(x => x.root == root + LanguageId && x.id != Id);
			}
		}
	#endif
	}
}