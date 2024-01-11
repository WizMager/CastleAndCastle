using Core.LoadingProcessor.Impls;
using Core.SceneLoading;
using UnityEngine.SceneManagement;
using Zenject;

namespace Ecs.Core.SceneLoading.SceneLoading.Impls
{
    public class SceneLoadingManager : ISceneLoadingManager
    {
        private readonly SignalBus _signalBus;
        private LoadingProcessor _processor;
        private ELevelName _currentLevel;

        public SceneLoadingManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void LoadGameLevel(ELevelName levelName)
        {
            _processor = new LoadingProcessor();
            _processor
                .AddProcess(new OpenLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(ELevelName.GAME, LoadSceneMode.Additive))
                .AddProcess(new LoadingProcess(levelName, LoadSceneMode.Additive))
                .AddProcess(new SetActiveSceneProcess(ELevelName.GAME))
                .AddProcess(new UnloadProcess(ELevelName.GAME));
                
            if (!string.IsNullOrWhiteSpace(_currentLevel.ToString()))
            {
                var lastScene = SceneManager.GetSceneByName(_currentLevel.ToString());
                if(lastScene.IsValid() && lastScene.isLoaded)
                    _processor.AddProcess(new UnloadProcess(_currentLevel));
            }
            
            _processor.AddProcess(new RunContextProcess("LevelContext"))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public void LoadGameFromSplash()
        {
            _processor = new LoadingProcessor();
            _processor
                .AddProcess(new OpenLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(ELevelName.GAME, LoadSceneMode.Additive))
                //.AddProcess(new LoadingProcess(ELevelName.FirstLevel, LoadSceneMode.Additive))
                .AddProcess(new SetActiveSceneProcess(ELevelName.GAME))
                .AddProcess(new UnloadProcess(ELevelName.SPLASH))
                .AddProcess(new RunContextProcess("GameContext"))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
            _currentLevel = ELevelName.FirstLevel;
        }

        public float GetProgress()
        {
            return _processor.Progress;
        }
    }
}