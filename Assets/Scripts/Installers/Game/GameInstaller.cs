using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInitializeSystems();
            BindWindows();
            BindSystems();
            BindServices();
        }

        private void BindInitializeSystems()
        {
            
        }
        
        private void BindSystems()
        {
            
        }
        
        private void BindServices()
        {
            
        }

        private void BindWindows()
        {
            
        }
    }
}