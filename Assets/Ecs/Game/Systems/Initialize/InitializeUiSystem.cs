using System.Collections.Generic;
using Ecs.Utils.Interfaces;
using Game.Ui.Windows;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using SimpleUi.Signals;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 3000, nameof(EFeatures.Initialization))]
    public class InitializeUiSystem : IInitializeSystem
    {
        private readonly List<IUiInitialize> _uiInitializes;
        private readonly SignalBus _signalBus;

        public InitializeUiSystem(List<IUiInitialize> uiInitializes, SignalBus signalBus)
        {
            _uiInitializes = uiInitializes;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            foreach (var uiInitialize in _uiInitializes)
            {
                uiInitialize.Initialize();
            }
            
            _signalBus.OpenWindow<GameHudWindow>();
        }
    }
}