using GameFramework.GameManagerBase.Common;
using RinoLocalize.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFramework.GameManagerBase.SOBase
{
	public abstract class IconIncludedData: InfoData
	{
		[HideLabel, PreviewField(70,ObjectFieldAlignment.Center)]
		[Required]
		[HorizontalGroup(LayoutConst.TopInfoLayout,200)]
		[PropertyOrder(-1),PropertySpace(20)]
		public Sprite Icon;
		
		public override bool IsDataLegal()
		{
			return Icon && base.IsDataLegal();
		}
	}
}