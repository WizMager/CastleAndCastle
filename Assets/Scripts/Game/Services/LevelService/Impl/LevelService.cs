using Ecs.Core.SceneLoading.SceneLoadingManager;
using UnityEngine;

namespace Game.Services.LevelService.Impl
{
    public class LevelService : ILevelService
    {
        private const string LEVEL_NUMBER_KEY = "LevelNumberKey";

        public LevelService()
        {
            CurrentLevelIndex = PlayerPrefs.GetInt(LEVEL_NUMBER_KEY, 0);
        }
        
        public int CurrentLevelIndex { get; private set; }

        public void NextLevel()
        {
            CurrentLevelIndex++;
            
            PlayerPrefs.SetInt(LEVEL_NUMBER_KEY, CurrentLevelIndex);
        }

        public string GetCurrentLevel()
        {
            //TODO: add list with levels name and check current level name by level index
            return "LevelScene";
        }
    }
}