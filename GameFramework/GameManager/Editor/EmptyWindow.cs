using Sirenix.OdinInspector.Editor;

namespace GameFramework.GameManager.Editor
{
	public class EmptyWindow: OdinMenuEditorWindow
	{
		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = new OdinMenuTree(true);
			return tree;
		}
	}
}