using System.Collections.Generic;
using Ecs.Commands;
using Ecs.Utils.Interfaces;
using Game.Ui;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using SimpleUi.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 50, nameof(EFeatures.Initialization))]
    public class GameInitializeSystem : IInitializeSystem
    {
        private readonly SignalBus _signalBus;
        private readonly ICommandBuffer _commandBuffer;
        private readonly List<IUiInitialize> _uiInitializes;

        public GameInitializeSystem(
            SignalBus signalBus,
            ICommandBuffer commandBuffer,
            List<IUiInitialize> uiInitializes)
        {
            _signalBus = signalBus;
            _commandBuffer = commandBuffer;
            _uiInitializes = uiInitializes;
        }
        
        public void Initialize()
        {
            InitializeUi(_uiInitializes);
            
            Observable.TimerFrame(100).Subscribe(_ =>
            {
                _signalBus.OpenWindow<GameWindow>();
            });
            
            DebugSpawnUnits();
        }

        private void InitializeUi(List<IUiInitialize> uiInitializes)
        {
            foreach (var ui in uiInitializes)
            {
                ui.Initialize();
            }
        }

        private void DebugSpawnUnits()
        {
            for (int i = 0; i < 4; i++)
            {
                _commandBuffer.SpawnUnit(new Vector3(-40 + i, 0.5f, 0), Quaternion.identity, EUnitType.MeleeUnit, false);
            }
            
            for (int i = 0; i < 2; i++)
            {
                _commandBuffer.SpawnUnit(new Vector3(-40 + i, 0.5f, 1 + i), Quaternion.identity, EUnitType.RangeUnit, false);
            }
            
            for (int i = 0; i < 3; i++)
            {
                _commandBuffer.SpawnUnit(new Vector3(40 - i, 0.5f, 0), Quaternion.identity, EUnitType.MeleeUnit, true);
            }
            
            for (int i = 0; i < 2; i++)
            {
                _commandBuffer.SpawnUnit(new Vector3(40 - i, 0.5f, 1 + i), Quaternion.identity, EUnitType.RangeUnit, true);
            }
        }
    }
}