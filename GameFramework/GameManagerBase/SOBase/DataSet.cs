using GameFramework.RinoUtility.Editor;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFramework.GameManagerBase.SOBase
{
	public class DataSet<T>: ScriptableObject where T: SODataBase
	{
		[ListDrawerSettings(OnTitleBarGUI = "DrawCustomRefreshButton", DraggableItems = false, NumberOfItemsPerPage = 20)]
		public List<T> Datas = new();

		/// <summary>
		///     根據傳入的 Data Id 回傳資料
		/// </summary>
		/// <param name="dataId">資料ID</param>
		/// <returns></returns>
		public T GetData(string dataId)
		{
			var data = Datas.Find(x => x.Id == dataId);

			if(data == null)
			{
				throw new ArgumentNullException($"找不到dataId: {dataId}");
			}

			return data;
		}

		/// <summary>
		///     回傳一筆隨機資料
		/// </summary>
		/// <returns></returns>
		public T GetRandomData()
		{
			if(Datas.Count == 0)
			{
				throw new ArgumentNullException($"找不到任何資料");
			}
			return Datas[Random.Range(0, Datas.Count)];
		}

	#if UNITY_EDITOR

		[UsedImplicitly]
		private void DrawCustomRefreshButton()
		{
			if(SirenixEditorGUI.ToolbarButton(EditorIcons.Refresh))
			{
				Datas = RinoEditorUtility.FindAssets<T>();
			}
		}

		public IEnumerable DrawDropDown()
		{
			return Datas.Select(data => new ValueDropdownItem(data.AssetName, data.Id));
		}

	#endif
	}
}