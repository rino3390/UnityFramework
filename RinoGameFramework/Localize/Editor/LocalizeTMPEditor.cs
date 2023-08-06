using TMPro.EditorUtilities;
using UnityEditor;

namespace RinoGameFramework.Localize.Editor
{
	[CustomEditor(typeof(LocalizeTMP))]
	public class LocalizeTMPEditor: TMP_BaseEditorPanel
	{
		protected override void DrawExtraSettings()
		{
			throw new System.NotImplementedException();
		}

		protected override bool IsMixSelectionTypes()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnUndoRedo()
		{
			throw new System.NotImplementedException();
		}
	}
}