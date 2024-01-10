using Core.LoadingProcessor.Impls;
using Ecs.Core.SceneLoading.SceneLoading;
using Ecs.Core.SceneLoading.SceneLoading.Impls;
using Zenject;

namespace Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LoadingProcessor>().AsSingle();
            Container.Bind<ISceneLoadingManager>().To<SceneLoadingManager>().AsSingle();
            
            SignalBusInstaller.Install(Container);
        }
    }
}