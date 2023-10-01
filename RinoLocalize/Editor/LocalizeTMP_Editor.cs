using RinoLocalize.Component;
using Sirenix.OdinInspector.Editor;
using TMPro.EditorUtilities;
using UnityEditor;

namespace RinoLocalize.Editor
{
	[CustomEditor(typeof(LocalizeTMP))]
	public class LocalizeTMP_Editor: TMP_EditorPanelUI
	{
		private InspectorProperty localizeStringId;

		private PropertyTree objectTree;
		private InspectorProperty useLocalize;
		private InspectorProperty createBtn;
		private InspectorProperty openPop;

		private void DrawSingle()
		{
			objectTree ??= PropertyTree.Create(serializedObject);
			localizeStringId = objectTree.GetPropertyAtPath("LocalizeStingId");
			useLocalize = objectTree.GetPropertyAtPath("UseLocalize");
			openPop = objectTree.GetPropertyAtPath("openPop");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			objectTree.Dispose();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			DrawSingle();
		}

		public override void OnInspectorGUI()
		{
			objectTree.BeginDraw(true);
			useLocalize.Draw();
			localizeStringId.Draw();
			openPop.Draw();
			objectTree.EndDraw();

			base.OnInspectorGUI();
		}
	}
}