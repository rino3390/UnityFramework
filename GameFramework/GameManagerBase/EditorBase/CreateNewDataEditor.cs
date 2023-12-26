using GameFramework.GameManagerBase.Extension;
using GameFramework.GameManagerBase.SOBase;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Linq;
using UnityEditor;
using GUID = GameFramework.RinoUtility.Misc.GUID;

namespace GameFramework.GameManagerBase.EditorBase
{
	public abstract class CreateNewDataEditor<T>: GameEditorMenu where T: SODataBase
	{
		protected abstract string dataRoot { get; }

		protected abstract string dataTypeLabel { get; }

		private string _dataRoot => dataRoot + "/";

		[UsedImplicitly]
		private string _dataTypeLabel => "新增" + dataTypeLabel;

		[BoxGroup("$_dataTypeLabel")]
		[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
		public T Data;

		[Required("程式端尚未實作資料整合方法")]
		[ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Hidden), HideLabel]
		[PropertySpace(10)]
		private DataSet<T> _dataSet;

		private readonly bool addAllDataForMenu;
		private readonly bool drawDelete;

		protected CreateNewDataEditor(bool addAllDataForMenu = true, bool drawDelete = true)
		{
			this.drawDelete = drawDelete;
			this.addAllDataForMenu = addAllDataForMenu;
		}

		protected override void Initialize()
		{
			SetNewData();

			var overViewType = typeof(DataSet<T>);
			var findAssets = AssetDatabase.FindAssets($"t:{overViewType.Name}");
			_dataSet = findAssets.Select(guid => AssetDatabase.LoadAssetAtPath<DataSet<T>>(AssetDatabase.GUIDToAssetPath(guid))).FirstOrDefault();
		}

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = SetTree().AddSelfMenu(this, dataTypeLabel);

			if(addAllDataForMenu)
			{
				tree.AddAllAssets<T>(_dataRoot, drawDelete);
			}
			return tree;
		}

		[BoxGroup("$_dataTypeLabel")]
		[OnInspectorGUI, ShowIf("@!Data.IsDataLegal()")]
		private void CreateNewDataInfoBox()
		{
			SirenixEditorGUI.ErrorMessageBox("資料尚未正確設定");
		}

		[BoxGroup("$_dataTypeLabel")]
		[Button("Create"), DisableIf("@!Data.IsDataLegal()"), GUIColor(0, 1, 0)]
		private void CreateNewData()
		{
			if(!Data.IsDataLegal()) return;

			AssetDatabase.CreateAsset(Data, "Assets/" + _dataRoot + Data.AssetName + ".asset");
			AssetDatabase.SaveAssets();

			SetNewData();
		}

		private void SetNewData()
		{
			Data = CreateInstance<T>();
			Data.Id = GUID.NewGuid();
			var root = _dataRoot.Split('/');
			Data.AssetName = root[^2] + " - " + Data.Id;
		}

		[ShowIf("@_dataSet == null")]
		private void CreateDataSet()
		{
			var dataSet = CreateInstance(typeof(DataSet<T>));
			AssetDatabase.CreateAsset(dataSet, "Assets/Game/Data/Set/" + typeof(T).Name + "DataSet.asset");
			AssetDatabase.SaveAssets();
			_dataSet = (DataSet<T>)dataSet;
		}
	}
}