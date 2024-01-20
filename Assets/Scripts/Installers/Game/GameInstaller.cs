using Ecs.Utils.LinkedEntityRepository.Impl;
using Ecs.Utils.SpawnService.Impl;
using Game.Providers.CameraProvider;
using Game.Providers.CameraProvider.Impl;
using Game.Services.InputService.Impl;
using Game.Services.PrefabPoolService.Impl;
using Game.Ui;
using Game.Ui.Windows;
using Game.Utils.Raycast.Impl;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] public Camera Camera;
        
        public override void InstallBindings()
        {
            DeclareSignals();
            BindWindows();
            BindServices();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<GameWindow>();
            Container.DeclareSignal<GameHudWindow>();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<GameWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameHudWindow>().AsSingle();
        }
        
        private void BindServices()
        {
            Container.Bind<ICameraProvider>().FromInstance(new SceneCameraProvider(Camera)).AsSingle();
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<LinkedEntityRepository>().AsSingle();
            Container.BindInterfacesTo<PrefabPoolService>().AsSingle();
            Container.BindInterfacesTo<UnityInputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<RayCastProvider>().AsSingle();
        }
    }
}