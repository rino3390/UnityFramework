using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RinoGameFramework.GameManager.DataScript
{
	public class DataOverview<T>: ScriptableObject where T: SODataBase
	{
		[ListDrawerSettings(OnTitleBarGUI = "DrawCustomRefreshButton", DraggableItems = false, NumberOfItemsPerPage = 20)]
		public List<T> Datas = new();

		/// <summary>
		///     根據傳入的 Data Id 回傳資料
		/// </summary>
		/// <param name="dataId">資料ID</param>
		/// <param name="force">為true時強制取得資料，若無法取得會拋錯</param>
		/// <returns></returns>
		public T GetData(string dataId, bool force = false)
		{
			var data = Datas.Find(x => x.Id == dataId);

			if(force && data == null)
			{
				throw new ArgumentNullException($"找不到dataId: {dataId}");
			}

			var defaultData = CreateInstance(typeof(T)) as T;
			defaultData!.Id = Guid.NewGuid().ToString();
			var returnData = data == null ? defaultData : data;
			return CreateDataInstance(returnData);
		}

		/// <summary>
		///     回傳一筆隨機資料
		/// </summary>
		/// <returns></returns>
		public T GetRandomData()
		{
			return CreateDataInstance(Datas[Random.Range(0, Datas.Count)]);
		}

		/// <summary>
		///     回傳新的實例避免資料在程式中被更動
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private static T CreateDataInstance(T data)
		{
			return Instantiate(data);
		}

	#if UNITY_EDITOR
		public void UpdateDataList()
		{
			Datas = GetAllData();
		}

		private List<T> GetAllData()
		{
			var type = typeof(T).Name;
			return AssetDatabase.FindAssets($"t:{type}")
								.Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
								.OrderBy(x => x.AssetName)
								.ToList();
		}

		[UsedImplicitly]
		private void DrawCustomRefreshButton()
		{
			if(SirenixEditorGUI.ToolbarButton(EditorIcons.Refresh))
			{
				UpdateDataList();
			}
		}

		public IEnumerable DrawDropDown()
		{
			return Datas.Select(data => new ValueDropdownItem(data.AssetName, data.Id));
		}

	#endif
	}
}