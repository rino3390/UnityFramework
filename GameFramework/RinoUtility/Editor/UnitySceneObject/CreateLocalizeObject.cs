using System;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

namespace GameFramework.RinoUtility.Editor.UnitySceneObject
{
	public class CreateLocalizeObject
	{
		[MenuItem("GameObject/UI/Localize/Text", false, 0)]
		public static void CreateLocalizeText(MenuCommand menuCommand)
		{
			CreateLocalizeObj("LocalizeText", typeof(TextMeshProUGUI), typeof(LocalizeStringEvent));
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