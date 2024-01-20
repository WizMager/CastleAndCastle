using Ecs.Utils.LinkedEntityRepository.Impl;
using Ecs.Utils.SpawnService.Impl;
using Game.Services.PrefabPoolService.Impl;
using Game.Ui;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            DeclareSignals();
            BindWindows();
            BindServices();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<GameWindow>();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<GameWindow>().AsSingle();
        }
        
        private void BindServices()
        {
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<LinkedEntityRepository>().AsSingle();
            Container.BindInterfacesTo<PrefabPoolService>().AsSingle();
        }
    }
}