using Ecs.Core.SceneLoading.SceneLoading;
using Zenject;

namespace Installers.Splash
{
    public class SplashManager : IInitializable
    {
        private readonly ISceneLoadingManager _sceneLoadingManager;

        public SplashManager(ISceneLoadingManager sceneLoadingManager)
        {
            _sceneLoadingManager = sceneLoadingManager;
        }
        
        public void Initialize()
        {
            _sceneLoadingManager.LoadGameFromSplash();
        }
    }
}