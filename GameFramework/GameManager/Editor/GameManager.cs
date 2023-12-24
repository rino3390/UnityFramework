using GameFramework.GameManager.DataScript;
using GameFramework.GameManagerBase.EditorBase;
using GameFramework.RinoUtility.Editor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFramework.GameManager.Editor
{
	public class GameManager: OdinMenuEditorWindow
	{
		object content; //用於讀取其他MenuTree的內容

		private GameManagerTabSetting tabSetting;
		private bool NeedRebuildTree; //重新繪製MenuTree
		private const int maxButtonsPerRow = 5;
		private int tabIndex;

		[MenuItem("Tools/GameManager")]
		public static void OpenWindow()
		{
			var window = GetWindow<GameManager>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 700);
		}

		private GameEditorMenu menu;

		//初始化
		protected override void Initialize()
		{
			tabSetting = RinoEditorUtility.FindAsset<GameManagerTabSetting>();

			if(tabSetting.Tabs.Count > 0)
			{
				tabIndex = 0;
				menu = CreateEditorMenuInstance(tabSetting.Tabs[tabIndex].CorrespondingWindow);
			}
		}

		//繪製整個Window，所以可以在這裡進行布局，姑且這樣認知
		protected override void OnGUI()
		{
			DrawWindowTab();
			MenuWidth = menu.MenuWidth;

			if(( NeedRebuildTree || menu.NeedRebuildTree ) && Event.current.type == EventType.Layout)
			{
				ForceMenuTreeRebuild();
				NeedRebuildTree = false;
				menu.NeedRebuildTree = false;
			}
			// DrawEditor(2);

			base.OnGUI();
		}

		//繪製右邊編輯視窗
		protected override void DrawEditors()
		{
			if(( NeedRebuildTree || menu.NeedRebuildTree ) && Event.current.type == EventType.Layout || Event.current.type == EventType.Repaint)
			{
				content = MenuTree.Selection.SelectedValue;
				NeedRebuildTree = false;
				menu.NeedRebuildTree = false;
			}

			DrawEditor(1);
		}

		//獲取要繪製的目標 (顯示在右邊編輯視窗)
		protected override IEnumerable<object> GetTargets()
		{
			List<object> targets = new List<object>();
			targets.Add(null);
			targets.Add(content);
			targets.Add(base.GetTarget());
			return targets;
		}

		//新增條目到菜單
		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = menu.menuTree;

			return tree;
		}

		protected override void OnBeginDrawEditors()
		{
			// menu.BeginDraw(MenuTree);
		}

		private void SwitchMenu<T>() where T: GameEditorMenu
		{
			// menu = CreateInstance<T>();
			NeedRebuildTree = true;
			MenuTree.Selection.Clear();
		}

		private void DrawWindowTab()
		{
			var buttonCount = 0;
			EditorGUILayout.BeginHorizontal();

			foreach(var tab in tabSetting.Tabs)
			{
				if(buttonCount >= maxButtonsPerRow)
				{
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					buttonCount = 0;
				}

				EditorGUILayout.BeginVertical(GUILayout.MaxHeight(30));

				if(SirenixEditorGUI.SDFIconButton(tab.TabName, 5f, tab.TabIcon)) { }

				EditorGUILayout.EndVertical();
				buttonCount++;
			}

			EditorGUILayout.EndHorizontal();
		}

		private GameEditorMenu CreateEditorMenuInstance(Type window)
		{
			return CreateInstance(window) as GameEditorMenu;
		}
	}
}