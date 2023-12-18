using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameFramework.RinoUtility.Editor
{
	public class RinoEditorUtility
	{
		public static List<T> FindAssets<T>() where T: Object
		{
			var data = AssetDatabase.FindAssets($"t:{typeof(T).Name}")
									.Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
									.ToList();
			return data;
		}

		public static T FindAsset<T>() where T: Object
		{
			var data = AssetDatabase.FindAssets($"t:{typeof(T).Name}")
									.Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
									.FirstOrDefault();
			return data;
		}

		public static void SaveSOData(SerializedObject serializedObject)
		{
			EditorUtility.SetDirty(serializedObject.targetObject);
			AssetDatabase.SaveAssets();
		}
	}
}