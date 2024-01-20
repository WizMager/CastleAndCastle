using System.Collections.Generic;
using Ecs.Utils;
using Game.Ui.Windows;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using SimpleUi.Signals;
using UniRx;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 3000, nameof(EFeatures.Initialization))]
    public class InitializeUiSystem : IInitializeSystem
    {
        private readonly List<IUiInitializable> _uiInitializables;
        private readonly SignalBus _signalBus;

        public InitializeUiSystem(List<IUiInitializable> uiInitializables, SignalBus signalBus)
        {
            _uiInitializables = uiInitializables;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            foreach (var uiInitializable in _uiInitializables)
            {
                uiInitializable.Initialize();
            }
            
            _signalBus.OpenWindow<GameHudWindow>();
        }
    }
}