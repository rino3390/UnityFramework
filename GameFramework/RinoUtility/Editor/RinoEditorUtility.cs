using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
			var data = AssetDatabase.FindAssets($"t:{typeof(T).Name}")
									.Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
									.FirstOrDefault();
			return data;
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

		public static List<Type> GetDerivedClasses<T>(bool searchAbstract = false, bool searchGeneric = false) where T : class
		{
			var inheritedClasses = AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany(s => s.GetTypes())
											.Where(x => searchAbstract ||!x.IsAbstract)
											.Where(x => searchGeneric || !x.IsGenericTypeDefinition)
											.Where(x => typeof(T).IsAssignableFrom(x))
											.ToList();
			return inheritedClasses;
		}
	}
}