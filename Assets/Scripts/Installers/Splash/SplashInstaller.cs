using UniRx;
using Zenject;

namespace Installers.Splash
{
    public class SplashInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MainThreadDispatcher.Initialize();

            Container.BindInterfacesAndSelfTo<SplashManager>().AsSingle().NonLazy();
        }
    }
}