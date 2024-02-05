using System;
using Db.Buildings;
using Ecs.Commands;
using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ecs.Game.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 140, nameof(EFeatures.Building))]
    public class CheckEnemyCoinsSystem : IUpdateSystem
    {
        private readonly GameContext _game;
        private readonly IBuildingSettingsBase _buildingSettingsBase;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly ICommandBuffer _commandBuffer;

        public CheckEnemyCoinsSystem(
            GameContext game,
            IBuildingSettingsBase buildingSettingsBase,
            IGameGroupUtils gameGroupUtils,
            ICommandBuffer commandBuffer
        )
        {
            _game = game;
            _buildingSettingsBase = buildingSettingsBase;
            _gameGroupUtils = gameGroupUtils;
            _commandBuffer = commandBuffer;
        }
        
        public void Update()
        {
            var enemyCastle = _game.EnemyCastleEntity;

            if (enemyCastle.HasTime) return;
            
            var cooldown = _buildingSettingsBase.EnemyBuildCooldown;
            enemyCastle.ReplaceTime(cooldown);
            
            var currentCoins = _game.EnemyCoins.Value;
            var minimalBuildPrice = enemyCastle.MinimalPrice.Value;
            
            if (currentCoins < minimalBuildPrice) return;

            using var _ = _gameGroupUtils.GetEmptyEnemyBuildingSlots(out var slots);
            
            if (slots.Count < 1) return;

            var randomSlotIndex = Random.Range(0, slots.Count);

            var chosenSlot = slots[randomSlotIndex];
            var slotUid = chosenSlot.Uid.Value;
            var buildingType = ChooseExpensiveBuilding();
            
            _commandBuffer.BuildEnemyBuilding(slotUid, buildingType);
        }

        private EBuildingType ChooseExpensiveBuilding()
        {
            var buildingPrice = -1;
            var buildingType = EBuildingType.Farm;
            

            foreach (var building in _buildingSettingsBase.GetAll())
            {
                if (buildingPrice >= building.Price) continue;

                buildingPrice = building.Price;
                buildingType = building.Type;
            }

            if (buildingPrice == -1)
            {
                throw new Exception("There is no building in list with price more then -1");
            }
            
            return buildingType;
        }
    }
}