using UniRx;
using Zenject;

namespace Installers.Splash
{
    public class SplashInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MainThreadDispatcher.Initialize();

            Container.Bind<SplashManager>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
        }
    }
}