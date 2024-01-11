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
            
        }
    }
}