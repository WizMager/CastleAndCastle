using Ecs.Core.SceneLoading.SceneLoadingManager;

namespace Game.Services.LevelService
{
    public interface ILevelService
    {
        int CurrentLevelIndex { get; }
        
        void NextLevel();
        string GetCurrentLevel();
    }
}