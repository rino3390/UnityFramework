using RinoGameFramework.GameManager.DataScript;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RinoGameFramework.GameManager.Editor.Drawer
{
	public class ItemDrawer<T> : OdinValueDrawer<T> where T : IconIncludedData
	{
		protected override void DrawPropertyLayout(GUIContent label)
		{
			var rect = EditorGUILayout.GetControlRect(label != null, 45);

			if( label != null )
			{
				rect.xMin = EditorGUI.PrefixLabel(rect.AlignCenterY(15), label).xMin;
			}
			else
			{
				rect = EditorGUI.IndentedRect(rect);
			}

			IconIncludedData includedData = this.ValueEntry.SmartValue;
			Texture texture = null;

			if( includedData )
			{
				texture = includedData.Icon.texture;
				GUI.Label(rect.AddXMin(50).AlignMiddle(16), EditorGUI.showMixedValue ? "-" : includedData.AssetName);
			}

			this.ValueEntry.WeakSmartValue = SirenixEditorFields.UnityPreviewObjectField(rect.AlignLeft(45), includedData, texture, this.ValueEntry.BaseValueType);
		}
	}
}