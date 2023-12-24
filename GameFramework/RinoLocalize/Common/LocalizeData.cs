using GameFramework.RinoUtility.Editor;
using RinoLocalize.DataScript;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace RinoLocalize.Common
{
	[Serializable]
	public class LocalizeData
	{
		[HideInInspector]
		public string Id;

		[LabelText("Id")]
		[Delayed]
		public string LanguageId;

		[LabelText("分類")]
		public string Root;

		[LabelText("本地化資料"), Space(5)]
		[ListDrawerSettings(HideAddButton = true, HideRemoveButton = true, DraggableItems = false, DefaultExpandedState = true)]
		[HideInTables, DisableContextMenu(true, true)]
		public List<LocalizeStruct> LocalizeValue = new();

		[HideInInspector]
		public LocalizeDataType DataType;

		public string DisplayName => string.IsNullOrEmpty(Root) ? LanguageId : Root + "/ " + LanguageId;

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			LocalizeData other = (LocalizeData)obj;
			return DisplayName == other.DisplayName;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, LanguageId, Root, LocalizeValue, (int)DataType);
		}

		protected bool Equals(LocalizeData other)
		{
			return DisplayName == other.DisplayName;
		}

	#if UNITY_EDITOR
		public bool CheckId
		{
			get
			{
				var data = RinoEditorUtility.FindAsset<LocalizeDataSet>();
				var root = string.IsNullOrEmpty(Root) ? "" : Root + "/";

				switch (DataType)
				{
					case LocalizeDataType.String:
						return data != null && !data.StringDataIdList.Any(x => x.root == root + LanguageId && x.id != Id);
					case LocalizeDataType.Image:
						return data != null && !data.ImageDataIdList.Any(x => x.root == root + LanguageId && x.id != Id);
					case LocalizeDataType.Audio:
						return data != null && !data.AudioDataIdList.Any(x => x.root == root + LanguageId && x.id != Id);
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	#endif
	}
}