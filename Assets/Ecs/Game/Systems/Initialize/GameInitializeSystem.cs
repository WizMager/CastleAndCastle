using Game.Ui;
using JCMG.EntitasRedux;
using SimpleUi.Signals;
using UniRx;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    public class GameInitializeSystem : IInitializeSystem
    {
        private readonly SignalBus _signalBus;

        public GameInitializeSystem(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            Observable.TimerFrame(100).Subscribe(_ =>
            {
                _signalBus.OpenWindow<GameWindow>();
            });
        }
    }
}