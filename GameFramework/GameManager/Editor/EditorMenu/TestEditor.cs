using GameFramework.GameManagerBase.EditorBase;
using GameFramework.GameManagerBase.Extension;
using Sirenix.OdinInspector.Editor;

namespace GameFramework.GameManager.Editor.EditorMenu
{
	public class TestEditor: GameEditorMenu
	{
		protected override OdinMenuTree BuildMenuTree()
		{
			var tree= SetTree();
			tree.CreateDeleteMenuTree<ActorData>("Game/Data/Actors","角色");
			tree.EnumerateTree().SortMenuItemsByName();
			return tree;
		}
	}
}