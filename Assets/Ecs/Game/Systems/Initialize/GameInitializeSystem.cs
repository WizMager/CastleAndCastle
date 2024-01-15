using Ecs.Commands;
using Game.Ui;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using SimpleUi.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    public class GameInitializeSystem : IInitializeSystem
    {
        private readonly SignalBus _signalBus;
        private readonly ICommandBuffer _commandBuffer;

        public GameInitializeSystem(
            SignalBus signalBus,
            ICommandBuffer commandBuffer
        )
        {
            _signalBus = signalBus;
            _commandBuffer = commandBuffer;
        }
        
        public void Initialize()
        {
            Observable.TimerFrame(100).Subscribe(_ =>
            {
                _signalBus.OpenWindow<GameWindow>();
            });
            
            DebugSpawnUnits();
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