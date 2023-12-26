using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace GameFramework.GameManagerBase.EditorBase
{
	public abstract class GameEditorMenu : OdinMenuEditorWindow
	{
		[HideInInspector]
		public bool NeedRebuildTree;
		public OdinMenuTree menuTree => BuildMenuTree();

		// public virtual void BeginDraw(OdinMenuTree tree) { }
		
		protected OdinMenuTree SetTree(float iconSize = 28, bool drawSearchToolbar=true, float width = 180f)
		{
			MenuWidth = width;
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