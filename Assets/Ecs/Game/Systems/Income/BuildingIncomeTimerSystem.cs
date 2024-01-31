using System.Collections.Generic;
using Db.Buildings;
using Ecs.Commands;
using Ecs.Core.Interfaces;
using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Income
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 150, nameof(EFeatures.Building))]
    public class BuildingIncomeTimerSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly ITimeProvider _timeProvider;
        private readonly IBuildingSettingsBase _buildingSettingsBase;
        private readonly ICommandBuffer _commandBuffer;

        public BuildingIncomeTimerSystem(
            IGameGroupUtils gameGroupUtils, 
            ITimeProvider timeProvider, 
            IBuildingSettingsBase buildingSettingsBase, 
            ICommandBuffer commandBuffer)
        {
            _gameGroupUtils = gameGroupUtils;
            _timeProvider = timeProvider;
            _buildingSettingsBase = buildingSettingsBase;
            _commandBuffer = commandBuffer;
        }
        
        public void Update()
        {
            using var disposable1 = _gameGroupUtils.GetBuildingsIncome(out var playerBuildings, true, e => e.HasIncomeTimer);
            using var disposable2 = _gameGroupUtils.GetBuildingsIncome(out var enemyBuildings, false, e => e.HasIncomeTimer);

            CheckIncome(playerBuildings, true);
            CheckIncome(enemyBuildings, false);
        }

        private void CheckIncome(List<GameEntity> buildings, bool isPlayerBuildings)
        {
            foreach (var entity in buildings)
            {
                var incomeTimer = entity.IncomeTimer.Value;
                var income = entity.Income.Value;
                var type = entity.BuildingType.Value;
                var settings = _buildingSettingsBase.Get(type);
                incomeTimer -= _timeProvider.DeltaTime;
                entity.ReplaceIncomeTimer(incomeTimer);
                
                Debug.Log($"BuildingIncomeTimerSystem incomeTimer: {incomeTimer}, _timeProvider.DeltaTime : {_timeProvider.DeltaTime}, isPlayer {isPlayerBuildings}");
                if (incomeTimer <= 0)
                {
                    _commandBuffer.AddCoins(income, isPlayerBuildings);
                    entity.ReplaceIncomeTimer(settings.IncomeTimer);
                }
            }
        }
    }
}