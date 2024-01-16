using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Editor.Header;
using Plugins.Extensions.InstallerGenerator.Editor.Models;
using Plugins.Extensions.InstallerGenerator.Editor.Sort;
using Plugins.Extensions.InstallerGenerator.Editor.Sort.Impls;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plugins.Extensions.InstallerGenerator.Editor
{
	public partial class EcsInstallerGeneratorWindow : EditorWindow
	{
		private List<ASortButtonDrawer> _sortButtonDrawers;

		private readonly List<ISortStrategy> _sortStrategies = new();
		private readonly List<AttributeRecord> _attributes = new();
		private readonly List<AttributeRecord> _current = new();

		private const string SaveKeyPath = "Ecs.InstallerGenerator.Path";
		private const string SaveKeySearchFolder = "Ecs.InstallerGenerator.SearchFolder";
		private const string SaveKeyDefault = "Ecs.InstallerGenerator.Default";

		private const string DefaultJson =
			"{\"Changed\":false,\"Type\":0,\"Priority\":2,\"Order\":100000,\"Name\":\"\"}";

		private string _savePath;
		private string _searchPath;
		private Vector2 _scroll;
		private int _filterPriority;
		private int _filterType;
		private int _sortOrder;
		private ESortType _sorting;
		private int _counter;
		private string _search;
		private bool _options;
		private AttributeChanges _default;

		private static bool _inProgress;

		[MenuItem("Tools/Entitas/Installer Generator Properties")]
		public static void Open()
		{
			var window = GetWindow<EcsInstallerGeneratorWindow>("Installer Generator");
			window.Show();
		}

		private void OnEnable()
		{
			wantsMouseEnterLeaveWindow = true;
			_options = false;
			_savePath = EditorPrefs.GetString(SaveKeyPath, "Ecs/Installers/");
			_searchPath = EditorPrefs.GetString(SaveKeySearchFolder, "Ecs/");
			_default = JsonUtility.FromJson<AttributeChanges>(EditorPrefs.GetString(SaveKeyDefault, DefaultJson));

			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (var i = 0; i < assemblies.Length; i++)
			{
				var types = assemblies[i].GetTypes();
				for (var j = 0; j < types.Length; j++)
				{
					var type = types[j];
					if (!type.IsDefined(typeof(InstallAttribute), false))
						continue;
					var attribute = type.GetCustomAttribute<InstallAttribute>(false);
					_attributes.Add(new AttributeRecord(type, attribute));
				}
			}

			InitializeSortButtons();

			foreach (var buttonDrawer in _sortButtonDrawers)
			{
				_sortStrategies.Add(buttonDrawer.SortStrategy);
			}
		}

		private void InitializeSortButtons()
		{
			var prioritySort = new PrioritySortStrategy();
			var orderSort = new OrderSortStrategy();
			var typeSort = new TypeSortStrategy();
			var nameSort = new NameSortStrategy();
			var featureSort = new FeatureSortStrategy();

			var prioritySortDrawer = new PrioritySortButtonDrawer(prioritySort, ButtonsStyle);
			var orderSortDrawer = new OrderSortButtonDrawer(orderSort, ButtonsStyle);
			var typeSortDrawer = new TypeSortButtonDrawer(typeSort, OrderStyle);
			var nameSortDrawer = new NameSortButtonDrawer(nameSort, NameStyle);
			var featureSortDrawer = new FeatureSortButtonDrawer(featureSort, FeatureStyle, _attributes);

			prioritySortDrawer.ResetOnClick(orderSort, typeSort, nameSort);
			orderSortDrawer.ResetOnClick(prioritySort, typeSort, nameSort);
			typeSortDrawer.ResetOnClick(prioritySort, orderSort, nameSort);
			nameSortDrawer.ResetOnClick(prioritySort, orderSort, typeSort);

			_sortButtonDrawers = new List<ASortButtonDrawer>
			{
				prioritySortDrawer,
				orderSortDrawer,
				typeSortDrawer,
				nameSortDrawer,
				featureSortDrawer,
			};
		}

		private void OnGUI()
		{
			Header();
			Options();
			Search();
			ListHeader();
			List();
			OpenAllScripts();
		}

		private void Header()
		{
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Options", EditorStyles.toolbarButton, ButtonsStyle))
				_options = !_options;
			GUILayout.Label("", EditorStyles.toolbarButton, GUILayout.ExpandWidth(true));
			if (GUILayout.Button("Generate", EditorStyles.toolbarButton, ButtonsStyle))
				Generate();
			GUILayout.EndHorizontal();
			GUILayout.Space(5);
		}

		private void Options()
		{
			if (!_options)
				return;
			GUILayout.Label("Options", EditorStyles.boldLabel);

			EditorGUI.BeginChangeCheck();

			GUILayout.Label("Paths", EditorStyles.boldLabel);
			_savePath = EditorGUILayout.TextField("Installers", _savePath);
			_searchPath = EditorGUILayout.TextField("Search scripts", _searchPath);

			GUILayout.Label("Default values", EditorStyles.boldLabel);
			_default.name = EditorGUILayout.TextField("Label", _default.name);
			_default.priority = (ExecutionPriority)EditorGUILayout.EnumPopup("Priority", _default.priority);
			_default.order = EditorGUILayout.IntField("Order", _default.order);

			if (EditorGUI.EndChangeCheck())
			{
				EditorPrefs.SetString(SaveKeyPath, _savePath);
				EditorPrefs.SetString(SaveKeySearchFolder, _searchPath);
				EditorPrefs.SetString(SaveKeyDefault, JsonUtility.ToJson(_default, false));
			}

			GUILayout.Space(10);
		}

		private void Search()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Search:", EditorStyles.toolbarButton, ButtonsStyle);
			EditorGUI.BeginChangeCheck();
			_search = GUILayout.TextField(_search, EditorStyles.toolbarTextField, GUILayout.ExpandWidth(true));
			if (EditorGUI.EndChangeCheck())
				ProcessSearch();
			GUILayout.Label($"{_current.Count}/{_attributes.Count}", EditorStyles.toolbarButton, ButtonsStyle);
			GUILayout.EndHorizontal();
		}


		private void ListHeader()
		{
			GUILayout.BeginHorizontal();

			ProcessSearch();
			ProcessSortButtonClick();
			ProcessSort();

			if (Button("Reset", ResetStyle))
			{
				foreach (var buttonDrawer in _sortButtonDrawers)
				{
					var sortStrategy = buttonDrawer.SortStrategy;
					sortStrategy.Reset();
				}
			}

			GUILayout.Space(20f);
			GUILayout.EndHorizontal();
		}

		private void ProcessSortButtonClick()
		{
			for (var i = 0; i < _sortButtonDrawers.Count; i++)
			{
				var buttonDrawer = _sortButtonDrawers[i];
				var sortStrategy = buttonDrawer.SortStrategy;
				if (!buttonDrawer.OnClick())
					continue;

				sortStrategy.Next();
				_sortStrategies.Remove(sortStrategy);
				_sortStrategies.Insert(_sortStrategies.Count, sortStrategy);
				break;
			}
		}

		private void ProcessSort()
		{
			foreach (var sortStrategy in _sortStrategies)
				sortStrategy.Sort(_current);
		}

		private void List()
		{
			_scroll = GUILayout.BeginScrollView(_scroll, false, true);
			foreach (var attribute in _current)
				Row(attribute);
			GUILayout.EndScrollView();
		}

		private void Row(AttributeRecord record)
		{
			GUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();

			record.Changes.priority = (ExecutionPriority)Enum(record.Changes.priority);
			record.Changes.order =
				EditorGUILayout.IntField(record.Changes.order, EditorStyles.miniTextField, OrderStyle);
			record.Changes.type = (ExecutionType)Enum(record.Changes.type);
			if (GUILayout.Button(record.Type.Name, EditorStyles.miniButton, GUILayout.ExpandWidth(true)))
			{
				GUI.changed = false;
				var selection = AssetDatabase.LoadAssetAtPath(FindFileForSelection(record.Type.Name), typeof(Object));
				Selection.activeObject = selection;
				AssetDatabase.OpenAsset(selection);
			}

			GUILayout.Label(string.Join("|", record.Features), EditorStyles.miniLabel, FeatureStyle);

			if (EditorGUI.EndChangeCheck())
				record.Changes.changed = true;

			if (record.Changes.changed)
			{
				if (GUILayout.Button("Save", EditorStyles.miniButton, ResetStyle))
				{
					ReplaceAttribute(
						FindFileForClass(record.Type.Name),
						record.Changes.type,
						record.Changes.name,
						record.Changes.priority,
						record.Changes.order
					);
					record.Changes.changed = false;
				}
			}
			else if (GUILayout.Button("Remove", EditorStyles.miniButton, ResetStyle))
			{
				Remove(FindFileForClass(record.Type.Name));
				AssetDatabase.Refresh();
			}

			GUILayout.EndHorizontal();
		}

		private void ProcessSearch()
		{
			_current.Clear();
			_current.AddRange(_attributes);

			if (string.IsNullOrEmpty(_search))
				return;

			var search = _search.ToLower();
			_current.RemoveAll(x => !x.Type.Name.ToLower().Contains(search));
		}

		private void OpenAllScripts()
		{
			if (GUILayout.Button("Open all scripts (up to 10)"))
			{
				foreach (var record in _current.Take(10))
				{
					var selection =
						AssetDatabase.LoadAssetAtPath(FindFileForSelection(record.Type.Name), typeof(Object));
					Selection.activeObject = selection;
					AssetDatabase.OpenAsset(selection);
				}
			}
		}

		[MenuItem("Tools/Entitas/Generate Installers &g")]
		public static void Generate()
		{
			if (_inProgress)
				return;

			EditorUtility.DisplayProgressBar("Ecs Installer", "Generate...", .1f);

			try
			{
				_inProgress = true;
				var installerTemplates = EcsInstallerGenerator.Generate();
				var path = EditorPrefs.GetString(SaveKeyPath, "Ecs/Installers/");

				foreach (var template in installerTemplates)
				{
					SaveToFile(template.GeneratedInstallerCode, $"{template.Name}.cs", path);
				}
			}
			finally
			{
				_inProgress = false;
				EditorUtility.ClearProgressBar();
			}

			AssetDatabase.Refresh();
		}
	}
}