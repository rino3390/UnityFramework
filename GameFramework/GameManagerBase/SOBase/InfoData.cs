using GameFramework.GameManagerBase.Common;
using Sirenix.OdinInspector;
using UnityEngine.Localization;

namespace GameFramework.GameManagerBase.SOBase
{
	public abstract class InfoData: SODataBase
	{
		[LabelText("顯示名稱")]
		[HorizontalGroup(LayoutConst.TopInfoLayout)]
		[VerticalGroup(LayoutConst.TopInfoLayout + "/1")]
		[PropertySpace(10, 10)]
		[Required("需要填寫名稱")]
		public LocalizedString DataName;
	}
}