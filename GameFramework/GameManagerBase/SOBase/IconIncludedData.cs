using GameFramework.GameManagerBase.Common;
using RinoLocalize.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFramework.GameManagerBase.SOBase
{
	public abstract class IconIncludedData: SODataBase
	{
		[HideLabel, PreviewField(70)]
		[Required]
		[HorizontalGroup(LayoutConst.TopInfoLayout,200)]
		[PropertyOrder(-1),PropertySpace(20)]
		public Sprite Icon;
		
		[LabelText("顯示名稱")]
		[HorizontalGroup(LayoutConst.TopInfoLayout)]
		[VerticalGroup(LayoutConst.TopInfoLayout + "/1")]
		[PropertySpace(10,10)]
		[ValueDropdown("@LocalizeDrawer.LocalizeStingIdDropDown()")]
		public string DataName;
		
		[HideLabel, ShowInInspector]
		private OpenCreateLocalizePopWindow openPop;

		public override bool CheckValidate()
		{
			return Icon && base.CheckValidate();
		}
	}
}