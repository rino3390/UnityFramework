using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace GameFramework.GameManager.Editor.Utility
{
	[CreateAssetMenu(fileName = "Tab", menuName = "GameManager/Tab", order = 0)]
	public class GameEditorMenu : OdinMenuEditorWindow
	{
		public bool NeedRebuildTree;
		// public virtual OdinMenuTree menuTree => BuildMenuTree();

		// public virtual void BeginDraw(OdinMenuTree tree) { }
		//
		protected OdinMenuTree SetTree(float iconSize = 28, bool drawSearchToolbar=true)
		{
			var tree = new OdinMenuTree(true)
			{
				DefaultMenuStyle =
				{
					IconSize = iconSize
				},
				Config =
				{
					DrawSearchToolbar = drawSearchToolbar
				}
			};
			return tree;
		}
		
		protected override OdinMenuTree BuildMenuTree()
		{
			return SetTree();
		}
	}
}