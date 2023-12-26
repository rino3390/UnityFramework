using GameFramework.GameManagerBase.Common;
using RinoLocalize.Common;
using Sirenix.OdinInspector;

namespace GameFramework.GameManagerBase.SOBase
{
	public abstract class InfoData: SODataBase
	{
		[LabelText("顯示名稱")]
		[HorizontalGroup(LayoutConst.TopInfoLayout)]
		[VerticalGroup(LayoutConst.TopInfoLayout + "/1")]
		[PropertySpace(10,10)]
		[ValueDropdown("@LocalizeDrawer.LocalizeStingIdDropDown()")]
		[Required("需要填寫名稱")]
		public string DataName;
		
		[HideLabel, ShowInInspector]
		[HorizontalGroup(LayoutConst.TopInfoLayout)]
		[VerticalGroup(LayoutConst.TopInfoLayout + "/1")]
		private OpenCreateLocalizePopWindow openPop = new OpenCreateLocalizePopWindow(LocalizeDataType.String, _ => { });
	}
}