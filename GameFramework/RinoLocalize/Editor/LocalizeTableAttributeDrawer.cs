using GameFramework.RinoUtility.Editor;
using RinoLocalize.Common;
using RinoLocalize.DataScript;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using SerializationUtility = Sirenix.Serialization.SerializationUtility;

namespace RinoLocalize.Editor
{
	public class LocalizeTableAttributeDrawer: OdinAttributeDrawer<LocalizeTableAttribute>
	{
		private IOrderedCollectionResolver resolver;
		private LocalPersistentContext<bool> isPagingExpanded;
		private LocalPersistentContext<Vector2> scrollPos;
		private LocalPersistentContext<int> currPage;
		private GUITableRowLayoutGroup table;
		private HashSet<string> seenColumnNames;
		private List<Column> columns;
		private ObjectPicker picker;
		private int colOffset;
		private GUIContent indexLabel;
		private bool isReadOnly;
		private int indexLabelWidth;
		private Rect columnHeaderRect;
		private GUIPagingHelper paging;
		private bool drawAsList = true;
		private bool isFirstFrame = true;
		private LanguageList languageList;

		/// <summary>
		///     Determines whether this instance [can draw attribute property] the specified property.
		/// </summary>
		protected override bool CanDrawAttributeProperty(InspectorProperty property) => property.ChildResolver is IOrderedCollectionResolver;

		/// <summary>Initializes this instance.</summary>
		protected override void Initialize()
		{
			languageList = RinoEditorUtility.FindAsset<LanguageList>();

			languageList.OnLanguageAdd += _ => Repaint();
			languageList.OnLanguageChange += (_, _) => Repaint();
			languageList.OnLanguageInsert += (_, _) => Repaint();
			languageList.OnLanguageRemove += _ => Repaint();

			Repaint();
		}

		private void Repaint()
		{
			var languageName = languageList.LanguageName.Select(x => x.Language).ToList();

			isReadOnly = Attribute.IsReadOnly || !Property.ValueEntry.IsEditable;
			indexLabelWidth = (int)SirenixGUIStyles.Label.CalcSize(new GUIContent("100")).x + 15;
			indexLabel = new GUIContent();
			colOffset = 0;
			seenColumnNames = new HashSet<string>();
			table = new GUITableRowLayoutGroup();
			table.MinScrollViewHeight = Attribute.MinScrollViewHeight;
			table.MaxScrollViewHeight = Attribute.MaxScrollViewHeight;
			resolver = Property.ChildResolver as IOrderedCollectionResolver;
			scrollPos = this.GetPersistentValue("scrollPos", Vector2.zero);
			currPage = this.GetPersistentValue<int>("currPage");
			isPagingExpanded = this.GetPersistentValue<bool>("expanded");
			columns = new List<Column>(10);
			paging = new GUIPagingHelper();
			paging.NumberOfItemsPerPage = Attribute.NumberOfItemsPerPage > 0 ?
											  Attribute.NumberOfItemsPerPage :
											  GlobalConfig<GeneralDrawerConfig>.Instance.NumberOfItemsPrPage;
			paging.IsExpanded = isPagingExpanded.Value;
			paging.IsEnabled = GlobalConfig<GeneralDrawerConfig>.Instance.ShowPagingInTables || Attribute.ShowPaging;
			paging.CurrentPage = currPage.Value;

			Property.ValueEntry.OnChildValueChanged += OnChildValueChanged;
			if (Attribute.AlwaysExpanded) Property.State.Expanded = true;
			int cellPadding = Attribute.CellPadding;
			if (cellPadding > 0)
				table.CellStyle = new GUIStyle
				{
					padding = new RectOffset(cellPadding, cellPadding, cellPadding, cellPadding)
				};
			GUIHelper.RequestRepaint();

			if (Attribute.ShowIndexLabels)
			{
				++colOffset;
				columns.Add(new Column(indexLabelWidth, true, false, null, ColumnType.Index));
			}

			if (isReadOnly) return;

			foreach (var language in languageName)
			{
				var col = new Column(80, false, true, language, ColumnType.Language);
				columns.Add(col);
				col.NiceName = language;
			}

			columns.Add(new Column(22, true, false, null, ColumnType.DeleteButton));
		}

		/// <summary>Draws the property layout.</summary>
		protected override void DrawPropertyLayout(GUIContent label)
		{
			if (drawAsList)
			{
				if (GUILayout.Button("以表格呈現")) drawAsList = false;
				CallNextDrawer(label);
			}
			else
			{
				if (GUILayout.Button("以清單呈現")) drawAsList = !drawAsList;

				picker = ObjectPicker.GetObjectPicker(this, resolver.ElementType);
				paging.Update(resolver.MaxCollectionLength);
				currPage.Value = paging.CurrentPage;
				isPagingExpanded.Value = paging.IsExpanded;
				Rect rect = SirenixEditorGUI.BeginIndentedVertical(SirenixGUIStyles.PropertyMargin);
				if (!Attribute.HideToolbar) DrawToolbar(label);

				if (Attribute.AlwaysExpanded)
				{
					Property.State.Expanded = true;
					DrawColumnHeaders();
					DrawTable();
				}
				else
				{
					if (SirenixEditorGUI.BeginFadeGroup(this, Property.State.Expanded) && Property.Children.Count > 0)
					{
						DrawColumnHeaders();
						DrawTable();
					}

					SirenixEditorGUI.EndFadeGroup();
				}

				SirenixEditorGUI.EndIndentedVertical();
				if (Event.current.type == EventType.Repaint) SirenixEditorGUI.DrawBorders(rect, 1, 1, Attribute.HideToolbar ? 0 : 1, 1);
				DropZone(rect);
				HandleObjectPickerEvents();
				if (Event.current.type != EventType.Repaint) return;

				isFirstFrame = false;
			}
		}

		private void OnChildValueChanged(int index)
		{
			IPropertyValueEntry valueEntry = Property.Children[index].ValueEntry;
			if (valueEntry == null || !typeof(ScriptableObject).IsAssignableFrom(valueEntry.TypeOfValue)) return;

			for (int index1 = 0; index1 < valueEntry.ValueCount; ++index1)
			{
				Object weakValue = valueEntry.WeakValues[index1] as Object;
				if ((bool)weakValue) EditorUtility.SetDirty(weakValue);
			}
		}

		private void DropZone(Rect rect)
		{
			if (isReadOnly) return;

			EventType type = Event.current.type;

			switch (type)
			{
				case EventType.DragUpdated:
				case EventType.DragPerform:
					if (!rect.Contains(Event.current.mousePosition)) break;

					Object[] objectArray = null;
					if (DragAndDrop.objectReferences.Any(n => n != null && resolver.ElementType.IsAssignableFrom(n.GetType())))
						objectArray = DragAndDrop.objectReferences.Where(x => x != null && resolver.ElementType.IsAssignableFrom(x.GetType()))
												 .Reverse()
												 .ToArray<Object>();
					else if (resolver.ElementType.InheritsFrom(typeof(UnityEngine.Component)))
						objectArray = (Object[])DragAndDrop.objectReferences.OfType<GameObject>()
														   .Select(x => x.GetComponent(resolver.ElementType))
														   .Where(x => x != null)
														   .Reverse()
														   .ToArray<UnityEngine.Component>();
					else if (resolver.ElementType.InheritsFrom(typeof(Sprite))
							 && DragAndDrop.objectReferences.Any(n => n is Texture2D && AssetDatabase.Contains(n)))
						objectArray = (Object[])DragAndDrop.objectReferences.OfType<Texture2D>()
														   .Select(x => AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(x)))
														   .Where((Func<Sprite, bool>)( x => x != null ))
														   .Reverse<Sprite>()
														   .ToArray<Sprite>();
					if (objectArray == null || objectArray.Length == 0) break;

					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					Event.current.Use();
					if (type != EventType.DragPerform) break;

					DragAndDrop.AcceptDrag();

					foreach (Object @object in objectArray)
					{
						object[] values = new object[Property.ParentValues.Count];
						for (int index = 0; index < values.Length; ++index) values[index] = @object;
						resolver.QueueAdd(values);
					}

					break;
			}
		}

		private void AddColumns(int rowIndexFrom, int rowIndexTo)
		{
			if (Event.current.type != EventType.Layout) return;

			for (int index1 = rowIndexFrom; index1 < rowIndexTo; ++index1)
			{
				int num = 0;
				InspectorProperty child1 = Property.Children[index1];

				for (int index2 = 0; index2 < child1.Children.Count; ++index2)
				{
					InspectorProperty child2 = child1.Children[index2];

					if (seenColumnNames.Add(child2.Name))
					{
						if (GetColumnAttribute<HideInTablesAttribute>(child2) != null)
						{
							++num;
						}
						else
						{
							bool preserveWidth = false;
							bool resizable = true;
							bool flag = true;
							int minWidth = Attribute.DefaultMinColumnWidth;
							TableColumnWidthAttribute columnAttribute = GetColumnAttribute<TableColumnWidthAttribute>(child2);

							if (columnAttribute != null)
							{
								preserveWidth = !columnAttribute.Resizable;
								resizable = columnAttribute.Resizable;
								minWidth = columnAttribute.Width;
								flag = false;
							}

							var labelText = child2.GetAttribute<LabelTextAttribute>();

							Column column = new Column(minWidth, preserveWidth, resizable, child2.Name, ColumnType.Property)
							{
								NiceName = labelText == null ? child2.NiceName : labelText.Text
							};
							column.NiceNameLabelWidth = (int)SirenixGUIStyles.Label.CalcSize(new GUIContent(column.NiceName)).x;
							column.PreferWide = flag;
							columns.Insert(Math.Min(index2 + colOffset - num, columns.Count), column);
							GUIHelper.RequestRepaint();
						}
					}
				}
			}
		}

		private void DrawToolbar(GUIContent label)
		{
			Rect rect1 = GUILayoutUtility.GetRect(0.0f, 22f);
			bool flag = Event.current.type == EventType.Repaint;
			if (flag) SirenixGUIStyles.ToolbarBackground.Draw(rect1, GUIContent.none, 0);

			paging.DrawToolbarPagingButtons(ref rect1, Property.State.Expanded, true);
			if (label == null) label = GUIHelper.TempContent("");
			Rect rect4 = rect1;
			rect4.x += 5f;
			rect4.y += 3f;
			rect4.height = 16f;

			if (Property.Children.Count > 0)
			{
				GUIHelper.PushHierarchyMode(false);
				if (Attribute.AlwaysExpanded)
					GUI.Label(rect4, label);
				else
					Property.State.Expanded = SirenixEditorGUI.Foldout(rect4, Property.State.Expanded, label);
				GUIHelper.PushHierarchyMode(true);
			}
			else
			{
				if (!flag) return;

				GUI.Label(rect4, label);
			}
		}

		private void DrawColumnHeaders()
		{
			if (Property.Children.Count == 0) return;

			this.columnHeaderRect = GUILayoutUtility.GetRect(0.0f, 21f);
			++this.columnHeaderRect.height;
			--this.columnHeaderRect.y;

			if (Event.current.type == EventType.Repaint)
			{
				SirenixEditorGUI.DrawBorders(this.columnHeaderRect, 1);
				EditorGUI.DrawRect(this.columnHeaderRect, SirenixGUIStyles.ColumnTitleBg);
			}

			this.columnHeaderRect.width -= this.columnHeaderRect.width - table.ContentRect.width;
			GUITableUtilities.ResizeColumns(this.columnHeaderRect, columns);
			if (Event.current.type != EventType.Repaint) return;

			GUITableUtilities.DrawColumnHeaderSeperators(this.columnHeaderRect, columns, SirenixGUIStyles.BorderColor);
			Rect columnHeaderRect = this.columnHeaderRect;

			for (int index = 0; index < columns.Count; ++index)
			{
				Column column = columns[index];
				if (columnHeaderRect.x > (double)this.columnHeaderRect.xMax) break;

				columnHeaderRect.width = column.ColWidth;
				columnHeaderRect.xMax = Mathf.Min(this.columnHeaderRect.xMax, columnHeaderRect.xMax);
				if (column.NiceName != null) GUI.Label(columnHeaderRect, column.NiceName, SirenixGUIStyles.LabelCentered);
				columnHeaderRect.x += column.ColWidth;
			}
		}

		private void DrawTable()
		{
			GUIHelper.PushHierarchyMode(false);
			table.DrawScrollView = Attribute.DrawScrollView && ( paging.IsExpanded || !paging.IsEnabled );
			table.ScrollPos = scrollPos.Value;
			table.BeginTable(paging.EndIndex - paging.StartIndex);
			AddColumns(table.RowIndexFrom, table.RowIndexTo);
			DrawListItemBackGrounds();
			float xOffset = 0.0f;

			for (int index = 0; index < columns.Count; ++index)
			{
				Column column = columns[index];
				int width = (int)column.ColWidth;
				if (isFirstFrame && column.PreferWide) width = 200;
				table.BeginColumn((int)xOffset, width);
				GUIHelper.PushLabelWidth(width * 0.3f);
				xOffset += column.ColWidth;

				for (int rowIndexFrom = table.RowIndexFrom; rowIndexFrom < table.RowIndexTo; ++rowIndexFrom)
				{
					table.BeginCell(rowIndexFrom);
					DrawCell(column, rowIndexFrom);
					table.EndCell(rowIndexFrom);
				}

				GUIHelper.PopLabelWidth();
				table.EndColumn();
			}

			DrawRightClickContextMenuAreas();
			table.EndTable();
			scrollPos.Value = table.ScrollPos;
			DrawColumnSeperators();
			GUIHelper.PopHierarchyMode();
			if (columns.Count <= 0 || columns[0].ColumnType != ColumnType.Index) return;

			columns[0].ColWidth = indexLabelWidth;
			columns[0].MinWidth = indexLabelWidth;
		}

		private void DrawColumnSeperators()
		{
			if (Event.current.type != EventType.Repaint) return;

			Color borderColor = SirenixGUIStyles.BorderColor;
			borderColor.a *= 0.4f;
			GUITableUtilities.DrawColumnHeaderSeperators(table.OuterRect, columns, borderColor);
		}

		private void DrawListItemBackGrounds()
		{
			if (Event.current.type != EventType.Repaint) return;

			for (int rowIndexFrom = table.RowIndexFrom; rowIndexFrom < table.RowIndexTo; ++rowIndexFrom)
			{
				EditorGUI.DrawRect(table.GetRowRect(rowIndexFrom),
								   rowIndexFrom % 2 == 0 ? SirenixGUIStyles.ListItemColorEven : SirenixGUIStyles.ListItemColorOdd);
			}
		}

		private void DrawRightClickContextMenuAreas()
		{
			for (int rowIndexFrom = table.RowIndexFrom; rowIndexFrom < table.RowIndexTo; ++rowIndexFrom)
			{
				Rect rowRect = table.GetRowRect(rowIndexFrom);
				Property.Children[rowIndexFrom].Update();
				PropertyContextMenuDrawer.AddRightClickArea(Property.Children[rowIndexFrom], rowRect);
			}
		}

		private void DrawCell(Column col, int rowIndex)
		{
			rowIndex += paging.StartIndex;

			if (col.ColumnType == ColumnType.Index)
			{
				Rect rect = GUILayoutUtility.GetRect(0.0f, 16f);
				rect.xMin += 5f;
				rect.width -= 2f;
				if (Event.current.type != EventType.Repaint) return;

				indexLabel.text = rowIndex.ToString();
				GUI.Label(rect, indexLabel, SirenixGUIStyles.Label);
				indexLabelWidth = Mathf.Max(indexLabelWidth, (int)SirenixGUIStyles.Label.CalcSize(indexLabel).x + 15);
			}
			else if (col.ColumnType == ColumnType.DeleteButton)
			{
				if (!SirenixEditorGUI.SDFIconButton(GUILayoutUtility.GetRect(20f, 20f).AlignCenter(13f, 13f),
													SdfIconType.X,
													IconAlignment.LeftOfText,
													SirenixGUIStyles.IconButton))
					return;

				resolver.QueueRemoveAt(rowIndex);
			}
			else if (col.ColumnType == ColumnType.Language)
			{
				var value = Property.Children[rowIndex].ValueEntry.WeakSmartValue as LocalizeData;
				var language = value!.LocalizeValue;
				var localizeStringStruct = language!.FirstOrDefault(x => x.LanguageType.Language == col.Name);

				if (localizeStringStruct == null)
				{
					return;
				}

				switch (value.DataType)
				{
					case LocalizeDataType.String:
						localizeStringStruct.StringValue = EditorGUI.TextField(EditorGUILayout.GetControlRect(), localizeStringStruct.StringValue);
						break;
					case LocalizeDataType.Image:
						localizeStringStruct.ImageValue =
							(Sprite)EditorGUI.ObjectField(EditorGUILayout.GetControlRect(), localizeStringStruct.ImageValue, typeof(Sprite), false);
						break;
					case LocalizeDataType.Audio:
						localizeStringStruct.AudioValue =
							(AudioClip)EditorGUI.ObjectField(EditorGUILayout.GetControlRect(), localizeStringStruct.AudioValue, typeof(AudioClip), false);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			else
			{
				if (col.ColumnType != ColumnType.Property) throw new NotImplementedException(col.ColumnType.ToString());

				var value = Property.Children[rowIndex].Children[col.Name];

				value.ValueEntry.WeakSmartValue = EditorGUI.TextField(EditorGUILayout.GetControlRect(), value.ValueEntry.WeakSmartValue as string);
			}
		}

		private void HandleObjectPickerEvents()
		{
			if (!picker.IsReadyToClaim || Event.current.type != EventType.Repaint) return;

			object obj = picker.ClaimObject();
			object[] values = new object[Property.Tree.WeakTargets.Count];
			values[0] = obj;
			for (int index = 1; index < values.Length; ++index) values[index] = SerializationUtility.CreateCopy(obj);
			resolver.QueueAdd(values);
		}

		private IEnumerable<InspectorProperty> EnumerateGroupMembers(InspectorProperty groupProperty)
		{
			for (int i = 0; i < groupProperty.Children.Count; ++i)
			{
				if (groupProperty.Children[i].Info.PropertyType != PropertyType.Group)
				{
					yield return groupProperty.Children[i];
				}
				else
				{
					foreach (InspectorProperty enumerateGroupMember in EnumerateGroupMembers(groupProperty.Children[i])) yield return enumerateGroupMember;
				}
			}
		}

		private T GetColumnAttribute<T>(InspectorProperty col) where T: System.Attribute =>
			col.Info.PropertyType != PropertyType.Group ?
				col.GetAttribute<T>() :
				EnumerateGroupMembers(col).Select(c => c.GetAttribute<T>()).FirstOrDefault(c => c != null);

		private enum ColumnType
		{
			Property,
			Index,
			DeleteButton,
			Language
		}

		private class Column: IResizableColumn
		{
			public readonly string Name;
			public float ColWidth;
			public float MinWidth;
			public readonly bool Preserve;
			public readonly bool Resizable;
			public string NiceName;
			public int NiceNameLabelWidth;
			public readonly ColumnType ColumnType;
			public bool PreferWide;

			public Column(int minWidth, bool preserveWidth, bool resizable, string name, ColumnType colType)
			{
				MinWidth = minWidth;
				ColWidth = minWidth;
				Preserve = preserveWidth;
				Name = name;
				ColumnType = colType;
				Resizable = resizable;
			}

			float IResizableColumn.ColWidth
			{
				get => ColWidth;
				set => ColWidth = value;
			}

			float IResizableColumn.MinWidth => MinWidth;

			bool IResizableColumn.PreserveWidth => Preserve;

			bool IResizableColumn.Resizable => Resizable;
		}
	}
}