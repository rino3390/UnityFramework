using GameFramework.GameManagerBase.SOBase;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace GameFramework.GameManagerBase.PopUp
{
	[Serializable]
	public class DeletePopUp
	{
		[Title("刪除確認")]
		[InlineEditor,ShowInInspector,ReadOnly]
		private SODataBase _soData;

		private string _path;
		private static OdinEditorWindow popupWindow;

		public DeletePopUp(SODataBase soData)
		{
			_soData = soData;
			_path = AssetDatabase.GetAssetPath(soData);
		}

		public static void OpenWindow(SODataBase soData, Rect rect)
		{
			popupWindow = OdinEditorWindow.InspectObjectInDropDown(new DeletePopUp(soData), rect, 300);
		}

		[Button]
		public void Delete()
		{
			AssetDatabase.DeleteAsset(_path);
			popupWindow.Close();
		}
	}
}
#endif