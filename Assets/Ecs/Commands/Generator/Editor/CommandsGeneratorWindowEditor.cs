using System.IO;
using Generator.Common;
using UnityEditor;
using UnityEngine;

namespace Ecs.Commands.Generator.Editor
{
	public class CommandsGeneratorWindow : EditorWindow
	{
		private const string DefaultPath = "Ecs/Commands";
		private const string GenerationPathKey = "CommandsGenerationPath";

		private static string _generationPathKey;
		private static string _defaultPath;
		private static string _generationPath;

		private static CommandExtensionsGenerator _generator;

		private static void InitGenerators()
		{
			if (_generator != null)
				return;

			_generator = new CommandExtensionsGenerator();
			_generationPathKey = GenerationPathKey;
			_defaultPath = DefaultPath;
		}

		[MenuItem("Tools/Commands/Settings")]
		public static void MiOpenWindow()
		{
			var window = GetWindowWithRect<CommandsGeneratorWindow>(new Rect(0, 0, 300, 100), false, "Commands generator");
			window.Show();
		}

		[MenuItem("Tools/Commands/Generate")]
		public static void GenerateProperties()
		{
			InitGenerators();
			_generationPath = EditorPrefs.GetString(_generationPathKey, _defaultPath);
			if (string.IsNullOrEmpty(_generationPath))
			{
				Debug.LogError($"[{nameof(CommandExtensionsGenerator)}] Generation path can't be empty!");
				return;
			}

			var directoryInfo = new DirectoryInfo(Path.Combine(Application.dataPath, _generationPath));

			var baseNamespace = _generationPath.Replace("/", ".");
			var file = _generator.Generate(directoryInfo, baseNamespace);
			Save(file);
			AssetDatabase.Refresh();
		}

		private void OnEnable()
		{
			InitGenerators();
			_generationPath = EditorPrefs.GetString(_generationPathKey, _defaultPath);
		}

		private void OnGUI()
		{
			DrawGenerationPath();
			DrawGenerateButton();
		}

		private static void DrawGenerationPath()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("GenerationPath");
			_generationPath = GUILayout.TextField(_generationPath);
			GUILayout.EndHorizontal();
		}

		private static void DrawGenerateButton()
		{
			if (GUILayout.Button("Save path")) EditorPrefs.SetString(_generationPathKey, _generationPath);

			if (GUILayout.Button("Generate Commands")) GenerateProperties();
		}

		private void OnDisable()
		{
			EditorPrefs.SetString(_generationPathKey, _generationPath);
		}

		private static void Save(GeneratedFile file)
		{
			var fileInfo = file.FileInfo;
			if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
				fileInfo.Directory.Create();
			if (fileInfo.Exists)
				fileInfo.Delete();
			File.WriteAllText(fileInfo.FullName, file.FileData);
		}
	}
}