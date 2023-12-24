using GameFramework.GameManager.Editor.EditorStruct;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace GameFramework.GameManager.DataScript
{
	public class GameManagerTabSetting: SerializedScriptableObject
	{
		public List<EditorTabData> Tabs = new();
	}
}