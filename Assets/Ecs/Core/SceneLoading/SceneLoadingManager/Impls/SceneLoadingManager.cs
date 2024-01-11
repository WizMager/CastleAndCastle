using Core.LoadingProcessor.Impls;
using Ecs.Core.SceneLoading.SceneLoading;
using Game.Services.LevelService;
using UnityEngine.SceneManagement;
using Zenject;

namespace Ecs.Core.SceneLoading.SceneLoadingManager.Impls
{
    public class SceneLoadingManager : ISceneLoadingManager
    {
        private readonly SignalBus _signalBus;
        private LoadingProcessor _processor;
        private readonly ILevelService _levelService;

        public SceneLoadingManager(
            SignalBus signalBus,
            ILevelService levelService
        )
        {
            _signalBus = signalBus;
            _levelService = levelService;
        }

        public void LoadGameLevel(ELevelName levelName)
        {
            var currentLevel = _levelService.GetCurrentLevel();
            
            _processor = new LoadingProcessor();
            _processor
                .AddProcess(new OpenLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(ELevelName.GAME, LoadSceneMode.Additive))
                .AddProcess(new LoadingProcess(levelName, LoadSceneMode.Additive))
                .AddProcess(new SetActiveSceneProcess(ELevelName.GAME))
                .AddProcess(new UnloadProcess(ELevelName.GAME));
            
            if (!string.IsNullOrWhiteSpace(currentLevel))
            {
                var lastScene = SceneManager.GetSceneByName(currentLevel);
                if(lastScene.IsValid() && lastScene.isLoaded)
                    _processor.AddProcess(new UnloadProcess(currentLevel));
            }
            
            _processor.AddProcess(new RunContextProcess("LevelContext"))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public void LoadGameFromSplash()
        {
            var currentLevel = _levelService.GetCurrentLevel();
            
            _processor = new LoadingProcessor();
            _processor
                .AddProcess(new OpenLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(ELevelName.GAME, LoadSceneMode.Additive))
                .AddProcess(new LoadingProcess(currentLevel, LoadSceneMode.Additive))
                .AddProcess(new SetActiveSceneProcess(ELevelName.GAME))
                .AddProcess(new UnloadProcess(ELevelName.SPLASH))
                .AddProcess(new RunContextProcess("GameContext"))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public float GetProgress()
        {
            return _processor.Progress;
        }
    }
}