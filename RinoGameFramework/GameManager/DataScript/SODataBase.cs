using RinoGameFramework.GameManager.Common;
using Sirenix.OdinInspector;

namespace RinoGameFramework.GameManager.DataScript
{
	public abstract class SODataBase: SerializedScriptableObject
	{
		[ReadOnly]
		[HorizontalGroup(LayoutConst.TopInfoLayout)]
		[VerticalGroup(LayoutConst.TopInfoLayout + "/1")]
		[PropertySpace(10)]
		public string Id;

		[HorizontalGroup(LayoutConst.TopInfoLayout)]
		[VerticalGroup(LayoutConst.TopInfoLayout + "/1")]
		[LabelText("檔案名稱")]
		[PropertySpace(10)]
		public string AssetName;
		
		public virtual bool CheckValidate()
		{
			return !string.IsNullOrEmpty(AssetName);
		}
	}
}