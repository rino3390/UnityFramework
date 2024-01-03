using GameFramework.GameManagerBase.Extension;
using GameFramework.GameManagerBase.SOBase;
using GameFramework.RinoUtility.Editor;
using JetBrains.Annotations;
using RinoLocalize.Common;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;
using GUID = GameFramework.RinoUtility.Misc.GUID;

namespace GameFramework.GameManagerBase.EditorBase
{
	public abstract class CreateNewDataEditor<T>: GameEditorMenu where T: SODataBase
	{
		protected abstract string dataRoot { get; }

		protected abstract string dataTypeLabel { get; }

		private string _dataRoot => dataRoot + "/";

		[UsedImplicitly]
		private string _createDataGroupLabel => "新增" + dataTypeLabel;

		[BoxGroup("$_createDataGroupLabel")]
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

		protected override void OnBeginDrawEditors()
		{
			SirenixEditorGUI.BeginHorizontalToolbar(MenuTree.Config.SearchToolbarHeight);
			{
				GUILayout.FlexibleSpace();
				if (SirenixEditorGUI.ToolbarButton(new GUIContent("新增本地化文字")))
				{
					var dataPop = new CreateLocalizeDataPop();
					var window = InspectObjectInDropDown(dataPop);
					dataPop.OpenWindow(LocalizeDataType.String,window);
				}
			}
			SirenixEditorGUI.EndHorizontalToolbar();
		}

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = SetTree().AddSelfMenu(this, dataTypeLabel);

			if(addAllDataForMenu)
			{
				tree.AddAllAssets<T>(dataTypeLabel,_dataRoot, drawDelete);
			}

			return tree;
		}

		[BoxGroup("$_createDataGroupLabel")]
		[OnInspectorGUI, ShowIf("@!Data.IsDataLegal()")]
		private void CreateNewDataInfoBox()
		{
			SirenixEditorGUI.ErrorMessageBox("資料尚未正確設定");
		}

		[BoxGroup("$_createDataGroupLabel")]
		[Button("Create"), DisableIf("@!Data.IsDataLegal()"), GUIColor(0.67f, 1f, 0.65f)]
		private void CreateNewData()
		{
			if(!Data.IsDataLegal()) return;
			RinoEditorUtility.CreateSOData(Data, _dataRoot + Data.AssetName);
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
			RinoEditorUtility.CreateSOData(dataSet, "Data/Set/" + typeof(T).Name + "DataSet");
			_dataSet = (DataSet<T>)dataSet;
		}
	}
}