using System.Collections.Generic;
using System.Linq;

namespace GameFramework.RinoUtility.Editor
{
	public class RinoEditorUtility
	{
		public static List<T> FindAssets<T>() where T: UnityEngine.Object
		{
			var data = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T).Name}")
								  .Select(guid => UnityEditor.AssetDatabase.LoadAssetAtPath<T>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid)))
								  .ToList();
			return data;
		}

		public static T FindAsset<T>() where T: UnityEngine.Object
		{
			var data = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T).Name}")
								  .Select(guid => UnityEditor.AssetDatabase.LoadAssetAtPath<T>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid)))
								  .FirstOrDefault();
			return data;
		}
	}
}