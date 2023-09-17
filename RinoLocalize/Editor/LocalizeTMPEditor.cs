using RinoLocalize.Component;
using Sirenix.OdinInspector.Editor;
using TMPro.EditorUtilities;
using UnityEditor;

namespace RinoLocalize.Editor
{
	[CustomEditor(typeof(LocalizeTMP))]
	public class LocalizeTMPEditor: TMP_EditorPanelUI
	{
		SerializedProperty customTextProp;

		protected override void OnEnable()
		{
			base.OnEnable();
			customTextProp = serializedObject.FindProperty("LocalizeStingId");
			DrawSingle();
		}

		public override void OnInspectorGUI()
		{
			myObjectTree.BeginDraw(true);
			property1.Draw();
			myObjectTree.EndDraw();

			base.OnInspectorGUI();
		}

		PropertyTree myObjectTree;
		private InspectorProperty property1;

		void DrawSingle()
		{
			if (this.myObjectTree == null)
			{
				this.myObjectTree = PropertyTree.Create(serializedObject);
			}
			property1 = myObjectTree.GetPropertyAtPath("LocalizeStingId");

		}
	}
}