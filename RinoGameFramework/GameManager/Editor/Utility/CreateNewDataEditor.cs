#if UNITY_EDITOR
using JetBrains.Annotations;
using RinoGameFramework.GameManager.DataScript;
using RinoGameFramework.Utility;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RinoGameFramework.GameManager.Editor.Utility
{
	public class CreateNewDataEditor<T> where T: SODataBase
	{
		private readonly string _dataRoot;

		[UsedImplicitly]
		private readonly string _dataType;

		[BoxGroup("$_dataType")]
		[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
		public T Data;

		[Required("程式端尚未實作資料整合方法")]
		[ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Hidden), HideLabel]
		[PropertySpace(10)]
		private readonly DataOverview<T> _dataOverview;

		public CreateNewDataEditor(string dataType, string dataRoot)
		{
			_dataRoot = dataRoot + "/";
			_dataType = "新增" + dataType;
			SetNewData();

			// 取得被實作的類
			var overViewType = typeof(DataOverview<T>).Assembly.GetTypes()
													  .FirstOrDefault(x => !x.IsAbstract && !x.IsGenericTypeDefinition && typeof(DataOverview<T>).IsAssignableFrom(x));

			if(overViewType == null)
			{
				return;
			}

			var findAssets = AssetDatabase.FindAssets($"t:{overViewType.Name}");
			_dataOverview = findAssets
							.Select(guid => AssetDatabase.LoadAssetAtPath<DataOverview<T>>(AssetDatabase.GUIDToAssetPath(guid)))
							.FirstOrDefault();

			if(_dataOverview == null)
			{
				//如果已實作繼承類就生成SO，否則回傳警告訊息
				var dataOverview = ScriptableObject.CreateInstance(overViewType);
				AssetDatabase.CreateAsset(dataOverview, "Assets/Game/Data/Overview/" + typeof(T).Name + "Overview.asset");
				AssetDatabase.SaveAssets();
				_dataOverview = (DataOverview<T>)dataOverview;
			}
		}

		[BoxGroup("$_dataType")]
		[Button("Create")]
		private void CreateNewData()
		{
			if(!Data.CheckValidate()) return;

			AssetDatabase.CreateAsset(Data, "Assets/" + _dataRoot + Data.AssetName + ".asset");
			AssetDatabase.SaveAssets();

			SetNewData();
		}

		private void SetNewData()
		{
			Data = ScriptableObject.CreateInstance<T>();
			Data.Id = RinoUtility.NewGuid();
			var root = _dataRoot.Split('/');
			Data.AssetName = root[^2] + " - " + Data.Id;
		}
	}
}
#endif