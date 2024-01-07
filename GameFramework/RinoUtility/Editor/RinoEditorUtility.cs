using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

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
			return FindAssets<T>().FirstOrDefault();
		}

		public static List<T> FindAssetsWithInheritance<T>() where T: Object
		{
			var type = GetDerivedClasses<T>().FirstOrDefault();
			var data = AssetDatabase.FindAssets($"t:{type}")
									.Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
									.ToList();
			return data;
		}
		
		public static T FindAssetWithInheritance<T>() where T: Object
		{
			return FindAssetsWithInheritance<T>().FirstOrDefault();
		}

		public static void SaveSOData(Object serializedObject)
		{
			EditorUtility.SetDirty(serializedObject);
			AssetDatabase.SaveAssets();
		}

		public static void CreateSOData(ScriptableObject data, string path)
		{
			var dir = "Assets/" + path;
			CreateDirectoryIfNotExist(dir);
			AssetDatabase.CreateAsset(data, dir + ".asset");
			AssetDatabase.SaveAssets();
		}

		public static void CreateDirectoryIfNotExist(string dir)
		{
			var directoryName = Path.GetDirectoryName(dir);

			if(!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName!);
			}
		}

		public static List<Type> GetDerivedClasses<T>(bool searchAbstract = false, bool searchGeneric = false)
		{
			var inheritedClasses = AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany(s => s.GetTypes())
											.Where(x => searchAbstract || !x.IsAbstract)
											.Where(x => searchGeneric || !x.IsGenericTypeDefinition)
											.Where(x => typeof(T).IsAssignableFrom(x))
											.ToList();
			return inheritedClasses;
		}
	}
}