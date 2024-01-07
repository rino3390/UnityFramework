using GameFramework.GameManagerBase.SOBase;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace GameFramework.GameManager.Drawer
{
	internal class IconItemDrawer<T>: OdinValueDrawer<T> where T: IconIncludedData
	{
		protected override void DrawPropertyLayout(GUIContent label)
		{
			var rect = EditorGUILayout.GetControlRect(label != null, 45);

			if(label != null)
			{
				rect.xMin = EditorGUI.PrefixLabel(rect.AlignCenterY(15), label).xMin;
			}
			else
			{
				rect = EditorGUI.IndentedRect(rect);
			}

			IconIncludedData item = this.ValueEntry.SmartValue;
			Texture texture = null;

			if(item && item.Icon)
			{
				texture = item.Icon.texture;
				GUI.Label(rect.AddXMin(50).AlignMiddle(16), EditorGUI.showMixedValue ? "-" : item.AssetName);
			}
			else
			{
				GUI.Label(rect.AddXMin(50).AlignMiddle(16), item.AssetName);
			}

			this.ValueEntry.WeakSmartValue = SirenixEditorFields.UnityPreviewObjectField(rect.AlignLeft(45), item, texture, this.ValueEntry.BaseValueType);
		}
	}
}