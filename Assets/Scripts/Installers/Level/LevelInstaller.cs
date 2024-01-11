using Game.Providers.GameFieldProvider;
using Game.Providers.GameFieldProvider.Impl;
using Game.Utils;
using Zenject;

namespace Installers.Level
{
    public class LevelInstaller : MonoInstaller
    {
        public GameField gameField;

        public override void InstallBindings()
        {
            var fieldProvider = new GameFieldProvider
            {
                GameField = gameField
            };

            Container.Bind<IGameFieldProvider>().FromInstance(fieldProvider).AsSingle();
        }
    }
}