using GameFramework.GameManager.DataScript;
using GameFramework.GameManagerBase.EditorBase;
using GameFramework.RinoUtility.Editor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace GameFramework.GameManager.Editor
{
	public class GameManager: OdinMenuEditorWindow
	{
		private GameManagerTabSetting tabSetting;
		private const int maxButtonsPerRow = 5;
		private int tabIndex;

		private GameEditorMenu menu;

		[MenuItem("Tools/GameManager")]
		public static void OpenWindow()
		{
			var window = GetWindow<GameManager>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 700);
		}

		protected override void Initialize()
		{
			tabSetting = RinoEditorUtility.FindAsset<GameManagerTabSetting>();

			if(tabSetting.Tabs.Count > 0)
			{
				tabIndex = 0;
				menu = CreateEditorMenuInstance(tabSetting.Tabs[tabIndex].CorrespondingWindow);
			}
		}

		protected override void OnGUI()
		{
			DrawWindowTab();
			MenuWidth = menu.MenuWidth;

			if(Event.current.type == EventType.Layout)
			{
				ForceMenuTreeRebuild();
			}

			menu.Draw();
		}
		
		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = menu.menuTree;
		
			return tree;
		}
		
		private void SwitchMenu(GameEditorMenu window)
		{
			menu = window;
			menu.ForceMenuTreeRebuild();
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

				if(SirenixEditorGUI.SDFIconButton(tab.TabName, 5f, tab.TabIcon))
				{
					SwitchMenu(CreateEditorMenuInstance(tab.CorrespondingWindow));
				}

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