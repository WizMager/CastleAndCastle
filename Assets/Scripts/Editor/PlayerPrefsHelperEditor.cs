using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PlayerPrefsHelperEditor
    {
        [MenuItem("Tools/SavedGameData/Delete PlayerPrefs")]
        public static void PlayerPrefsDeleteAll()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log($"[{nameof(PlayerPrefsHelperEditor)}] [✖_✖] Delete all PlayerPrefs");
        }

        [MenuItem("Tools/SavedGameData/Delete data in PersistentDataPath")]
        public static void DeleteGameDataInPersistentDataPath()
        {
            var pathData = Application.persistentDataPath;
            var files = Directory.GetFiles(pathData);
            
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                fileInfo.Delete();
            }

            Debug.Log($"[{nameof(PlayerPrefsHelperEditor)}] [✖_✖] Delete all in dir: {pathData}");
        }

        [MenuItem("Tools/SavedGameData/Delete saved json data in PersistentDataPath")]
        public static void DeleteSavedJsonData()
        {
            var pathData = Application.persistentDataPath;
            var path = Directory.GetParent(pathData);

            if (path == null) return;
                
            foreach (var file in path.GetFiles())
            {
                file.Delete();
            }
			
            Debug.Log($"[{nameof(PlayerPrefsHelperEditor)}] [✖_✖] Delete all in dir: {pathData}");
        }
		
        [MenuItem("Tools/SavedGameData/DeleteAll #&d")]
        public static void DeleteAll()
        {
            PlayerPrefsDeleteAll();
            DeleteGameDataInPersistentDataPath();
            DeleteSavedJsonData();
        }
    }
}