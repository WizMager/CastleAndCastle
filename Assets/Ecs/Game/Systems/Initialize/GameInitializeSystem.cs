using Ecs.Commands;
using Ecs.Game.Extensions;
using Game.Providers.GameFieldProvider;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 50, nameof(EFeatures.Initialization))]
    public class GameInitializeSystem : IInitializeSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;

        public GameInitializeSystem(
            ICommandBuffer commandBuffer,
            GameContext game
        )
        {
            _commandBuffer = commandBuffer;
            _game = game;
        }
        
        public void Initialize()
        {
            DebugSpawnUnits();
            
            _game.ReplacePlayerCoins(100);
            _game.ReplaceEnemyCoins(1000);
        }
        
        private void DebugSpawnUnits()
        {
            for (int i = 0; i < 3; i++)
            {
                _commandBuffer.SpawnUnit(new Vector3(-40 + i, 0.5f, 0), Quaternion.identity, EUnitType.MeleeUnit, false);
            }
            
            for (int i = 0; i < 2; i++)
            {
                _commandBuffer.SpawnUnit(new Vector3(-40 + i, 0.5f, 1 + i), Quaternion.identity, EUnitType.RangeUnit, false);
            }
            
            for (int i = 0; i < 4; i++)
            {
                _commandBuffer.SpawnUnit(new Vector3(43 - i, 0.5f, 0), Quaternion.identity, EUnitType.MeleeUnit, true);
            }
            
            for (int i = 0; i < 2; i++)
            {
                _commandBuffer.SpawnUnit(new Vector3(43 - i, 0.5f, 1 + i), Quaternion.identity, EUnitType.RangeUnit, true);
            }
        }
    }
}