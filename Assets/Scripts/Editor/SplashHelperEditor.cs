using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class SplashHelperEditor
    {
        [MenuItem("Tools/Open Splash Scene")]
        public static void OpenSplashScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Splash.unity", OpenSceneMode.Single);
        }
    }
}