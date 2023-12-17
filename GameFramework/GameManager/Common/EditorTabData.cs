using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace GameFramework.GameManager.Common
{
	[Serializable]
	public class EditorTabData
	{
		[FoldoutGroup("標籤設定", true)]
		public SdfIconType TabIcon;

		[FoldoutGroup("標籤設定")]
		public string TabName;

		[FoldoutGroup("Editor 設定", true)]
		[LabelText("左列繪製 Icon")]
		public bool HasIcon;

		[FoldoutGroup("Editor 設定")]
		[ShowIf("HasIcon"),LabelText("Icon 大小")]
		public float IconSize = 28;

		[FoldoutGroup("Editor 設定")]
		[ValueDropdown("GetWindowList"),LabelText("繪製視窗")]
		[Required]
		public OdinEditorWindow CorrespondingWindow;

		public IEnumerable GetWindowList()
		{
			var q = AppDomain.CurrentDomain.GetAssemblies()
							 .SelectMany(s => s.GetTypes())
							 .Where(x => !x.IsAbstract)
							 .Where(x => !x.IsGenericTypeDefinition)
							 .Where(x => typeof(OdinEditorWindow).IsAssignableFrom(x))
							 .Where(x => x.Namespace != null && !x.Namespace.StartsWith("Sirenix"))
							 .Select(x => new ValueDropdownItem(x.Name, ScriptableObject.CreateInstance(x)));
			return q;
		}
	}
}