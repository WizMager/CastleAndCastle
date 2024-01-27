using Db.Buildings;
using Ecs.Core.Interfaces;
using Ecs.Utils.Groups;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Commands.Systems.Income
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
            using var buildings = _gameGroupUtils.GetBuildingsIncome(out var buffer, 
                e => e.HasIncome && e.HasBuildingType && e.HasIncomeTimer);

            foreach (var entity in buffer)
            {
                var incomeTimer = entity.IncomeTimer.Value;
                var income = entity.Income.Value;
                var type = entity.BuildingType.Value;
                var settings = _buildingSettingsBase.Get(type);
                incomeTimer -= _timeProvider.DeltaTime;
                entity.ReplaceIncomeTimer(incomeTimer);
                
                Debug.Log($"BuildingIncomeTimerSystem incomeTimer: {incomeTimer}, _timeProvider.DeltaTime : {_timeProvider.DeltaTime}");
                if (incomeTimer <= 0)
                {
                    _commandBuffer.AddCoins(income);
                    entity.ReplaceIncomeTimer(settings.IncomeTimer);
                }
            }
        }
    }
}