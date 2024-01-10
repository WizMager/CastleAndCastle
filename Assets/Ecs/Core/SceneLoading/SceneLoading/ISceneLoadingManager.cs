using Core.SceneLoading;

namespace Ecs.Core.SceneLoading.SceneLoading
{
    public interface ISceneLoadingManager
    {
        void LoadGameLevel(ELevelName levelName);
        void LoadGameFromSplash();
        float GetProgress();
    }
}