using RinoGameFramework.Localize.DataScript;
using RinoGameFramework.Utility.Editor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RinoGameFramework.Localize.Editor
{
	public class LocalizeManagerEditorWindow: OdinEditorWindow
	{
		[ShowInInspector,InlineEditor(InlineEditorObjectFieldModes.Hidden)]
		private LocalizeDataSet localizeDataSet;

		protected override void OnEnable()
		{
			localizeDataSet = RinoEditorUtility.FindAsset<LocalizeDataSet>();
			Debug.Log($"Select");
		}

		[MenuItem("Tools/RinoWeeb/LocalizeManager")]
		public static void OpenWindow()
		{
			var window = GetWindow<LocalizeManagerEditorWindow>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 700);

		}
	}
}