using GameFramework.GameManagerBase.SOBase;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEditor;
using UnityEngine;
using GUID = GameFramework.RinoUtility.Misc.GUID;

namespace GameFramework.GameManagerBase.EditorBase
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
		private DataSet<T> _dataSet;

		public CreateNewDataEditor(string dataType, string dataRoot)
		{
			_dataRoot = dataRoot + "/";
			_dataType = "新增" + dataType;
			SetNewData();

			// 取得被實作的類
			var overViewType = typeof(DataSet<T>);

			var findAssets = AssetDatabase.FindAssets($"t:{overViewType.Name}");
			_dataSet = findAssets.Select(guid => AssetDatabase.LoadAssetAtPath<DataSet<T>>(AssetDatabase.GUIDToAssetPath((string)guid))).FirstOrDefault();
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
			Data.Id = GUID.NewGuid();
			var root = _dataRoot.Split('/');
			Data.AssetName = root[^2] + " - " + Data.Id;
		}

		[ShowIf("@_dataSet == null")]
		private void CreateDataSet()
		{
			var dataSet = ScriptableObject.CreateInstance(typeof(DataSet<T>));
			AssetDatabase.CreateAsset(dataSet, "Assets/Game/Data/Set/" + typeof(T).Name + "DataSet.asset");
			AssetDatabase.SaveAssets();
			_dataSet = (DataSet<T>)dataSet;
		}
	}
}