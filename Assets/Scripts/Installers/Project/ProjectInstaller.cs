using Core.LoadingProcessor.Impls;
using Ecs.Core.SceneLoading.SceneLoading;
using Ecs.Core.SceneLoading.SceneLoadingManager.Impls;
using Game.Services.LevelService.Impl;
using Game.Services.TimeProvider;
using Zenject;

namespace Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelService>().AsSingle();
            Container.BindInterfacesTo<LoadingProcessor>().AsSingle();
            Container.BindInterfacesTo<UnityTimerProvider>().AsSingle();
            Container.Bind<ISceneLoadingManager>().To<SceneLoadingManager>().AsSingle();
            
            SignalBusInstaller.Install(Container);
        }
    }
}