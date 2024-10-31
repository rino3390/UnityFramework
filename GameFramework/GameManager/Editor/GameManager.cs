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

		private GameEditorMenu menu;
		private bool hasWindow = false;

		[MenuItem("Tools/GameManager",priority = -10)]
		public static void OpenWindow()
		{
			var window = GetWindow<GameManager>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 700);
		}

		protected override void Initialize()
		{
			tabSetting = RinoEditorUtility.FindAsset<GameManagerTabSetting>();

			if(tabSetting == null)
			{
				var data = CreateInstance<GameManagerTabSetting>();
				RinoEditorUtility.CreateSOData(data, "Data/GameManager/Tab");
				tabSetting = data;
			}

			hasWindow = tabSetting!.Tabs.Count > 0;

			if(hasWindow)
			{
				menu = CreateEditorMenuInstance(tabSetting.Tabs[0].CorrespondingWindow);
			}
		}

		protected override void OnImGUI()
		{
			if(!hasWindow)
			{
				return;
			}

			DrawWindowTab();
			MenuWidth = menu.MenuWidth;

			menu.Draw();
		}

		/// <summary>
		/// 不複寫就會畫兩次GUI
		/// </summary>
		[Obsolete("Rename this to OnImGUI()", true)]
		protected override void OnGUI()
		{
		}

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = hasWindow ? menu.menuTree : new OdinMenuTree();

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