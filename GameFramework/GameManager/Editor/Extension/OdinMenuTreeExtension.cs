using GameFramework.GameManager.Common;
using GameFramework.GameManager.DataScript;
using GameFramework.GameManager.Editor.PopUp;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Linq;
using UnityEngine;

namespace GameFramework.GameManager.Editor.Extension
{
	public static class OdinMenuTreeExtension
	{
		public static void CreateDeleteMenuTree<T>(this OdinMenuTree tree,string path,string typeName) where T: SODataBase
		{
			var menuPath = path.Split('/').Last();
			tree.Add(menuPath, new CreateNewDataEditor<T>(typeName,path));
			tree.AddAllAssetsAtPath(menuPath, "Assets/"+path, typeof(T), true).ForEach(DrawDelete<T>);
			tree.EnumerateTree().Where(x => x.Value as T).ForEach(x => x.Name = ((T)x.Value).AssetName);
			
			//加上Icon
			tree.EnumerateTree().AddIcons<IconIncludedData>(x => x.Icon);
		}
		
		private static void DrawDelete<T>(OdinMenuItem menuItem) where T: SODataBase
		{
			menuItem.OnDrawItem += x =>
			{
				if( menuItem.Value == null || !(menuItem.Value as T) ) return;

				var buttonRect = new Rect(new Vector2(menuItem.Rect.width - 50, menuItem.Rect.y + 5), new Vector2(20, 20));
				
				if(GUI.Button(buttonRect, "") || SirenixEditorGUI.IconButton(buttonRect, EditorIcons.X) )
				{
					DeletePopUp.OpenWindow((T)menuItem.Value,buttonRect);
				}
			};
		}
	}
}