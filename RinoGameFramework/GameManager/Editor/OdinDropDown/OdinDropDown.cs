using RinoGameFramework.GameManager.DataScript.Language;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RinoGameFramework.GameManager.Editor.OdinDropDown
{
	public class OdinDropDown
	{
		

		public static IEnumerable Languages()
		{
			var data = GetDataOverview<LanguageDataOverview>();
			if(data != null) return data.LanguageDropDown();

			return null;
		}

		private static T GetDataOverview<T>() where T: ScriptableObject
		{
			var data = AssetDatabase.FindAssets($"t:{typeof(T).Name}")
									.Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
									.FirstOrDefault();
			return data;
		}
	}
}