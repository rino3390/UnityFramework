using GameFramework.GameManager.Common;
using RinoLocalize.RunTime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFramework.GameManager.DataScript
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
		public LanguageString DataName;

		public override bool CheckValidate()
		{
			return Icon && base.CheckValidate();
		}
	}
}