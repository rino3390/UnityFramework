using Sirenix.OdinInspector.Editor;

namespace GameFramework.GameManagerBase.EditorBase
{
	public abstract class GameEditorMenu: OdinMenuEditorWindow
	{
		public OdinMenuTree menuTree => BuildMenuTree();

		public void Draw()
		{
			base.OnImGUI();
		}

		protected OdinMenuTree SetTree(float iconSize = 28, bool drawSearchToolbar = true, float width = 220f)
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