using Game.Services.GameLevelViewProvider;
using Game.Services.GameLevelViewProvider.Impl;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameLevelView gameLevelView;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameLevelViewProvider>().To<GameLevelViewProvider>()
                .FromInstance(new GameLevelViewProvider(gameLevelView)).AsSingle();
        }
    }
}