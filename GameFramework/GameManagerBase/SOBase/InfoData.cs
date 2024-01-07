using GameFramework.GameManagerBase.Common;
using Sirenix.OdinInspector;

namespace GameFramework.GameManagerBase.SOBase
{
	public abstract class InfoData: SODataBase
	{
		[LabelText("顯示名稱")]
		[HorizontalGroup(LayoutConst.TopInfoLayout)]
		[VerticalGroup(LayoutConst.TopInfoLayout + "/1")]
		[PropertySpace(10, 10)]
		[ValueDropdown("@LocalizeDrawer.LocalizeStingIdDropDown()")]
		[Required("需要填寫名稱")]
		public string DataName;

		public override bool IsDataLegal()
		{
			return base.IsDataLegal() && !string.IsNullOrEmpty(DataName);
		}
	}
}