using GameFramework.GameManagerBase.PopUp;
using GameFramework.GameManagerBase.SOBase;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Linq;
using UnityEngine;

namespace GameFramework.GameManagerBase.Extension
{
	public static class OdinMenuTreeExtension
	{
		public static OdinMenuTree AddSelfMenu(this OdinMenuTree tree, object instance, string path = "Home")
		{
			tree.Add(path, instance);
			return tree;
		}

		public static OdinMenuTree AddAllAssets<T>(this OdinMenuTree tree, string path, bool drawDelete = true) where T: SODataBase
		{
			var menuPath = path.Split('/').Last();
			var menuItems = tree.AddAllAssetsAtPath(menuPath, "Assets/" + path, typeof(T), true);

			if(drawDelete)
			{
				menuItems.ForEach(DrawDelete<T>);
			}

			if(typeof(T) == typeof(IconIncludedData))
			{
				tree.EnumerateTree().AddIcons<IconIncludedData>(x => x.Icon);
			}
			return tree;
		}

	public static void DrawDelete<T>(OdinMenuItem menuItem) where T: SODataBase
		{
			menuItem.OnDrawItem += _ =>
			{
				if(menuItem.Value == null || !( menuItem.Value as T )) return;

				var buttonRect = new Rect(new Vector2(menuItem.Rect.width - 50, menuItem.Rect.y + 5), new Vector2(20, 20));

				if(GUI.Button(buttonRect, "") || SirenixEditorGUI.IconButton(buttonRect, EditorIcons.X))
				{
					DeletePopUp.OpenWindow((T)menuItem.Value, buttonRect);
				}
			};
		}
	}
}