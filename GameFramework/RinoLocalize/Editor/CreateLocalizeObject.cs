using RinoLocalize.Component;
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RinoLocalize.Editor
{
	public class CreateLocalizeObject
	{
		[MenuItem("GameObject/UI/RinoLocalize/Text", false, 0)]
		public static void CreateLocalizeText(MenuCommand menuCommand)
		{
			CreateLocalizeObj("LocalizeText", typeof(LocalizeTMP));
		}

		public static void CreateLocalizeObj(string name, params Type[] types)
		{
			var newObj = ObjectFactory.CreateGameObject(name, types);
			Place(newObj);
		}

		public static void Place(GameObject gameObject)
		{
			gameObject.transform.position = Vector3.zero;
			StageUtility.PlaceGameObjectInCurrentStage(gameObject);
			GameObjectUtility.EnsureUniqueNameForSibling(gameObject);
			Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
			Selection.activeObject = gameObject;
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}
}