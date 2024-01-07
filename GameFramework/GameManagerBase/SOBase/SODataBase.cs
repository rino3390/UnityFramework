using GameFramework.GameManagerBase.Common;
using GameFramework.RinoUtility.Misc;
using Sirenix.OdinInspector;

namespace GameFramework.GameManagerBase.SOBase
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
		[PropertySpace(10), ValidateInput("IsAssetNameLegal","名稱只能為英數（含減號底線）")]
		public string AssetName = "";

	#if UNITY_EDITOR
		private bool IsAssetNameLegal()
		{
			return !string.IsNullOrEmpty(AssetName) && RegexChecking.OnlyEnglishAndNum(AssetName);
		}

		public virtual bool IsDataLegal()
		{
			return IsAssetNameLegal();
		}
	#endif
	}
}