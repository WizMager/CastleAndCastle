using Db.Buildings;
using Ecs.Commands.Command.Buildings;
using Ecs.Game.Extensions;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Commands.Systems.Buildings
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 150, nameof(EFeatures.Building))]
    public class BuildEnemyBuildingSystem : ForEachCommandUpdateSystem<BuildEnemyBuildingCommand>
    {
        private readonly GameContext _game;
        private readonly IBuildingSettingsBase _buildingSettingsBase;
        
        public BuildEnemyBuildingSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IBuildingSettingsBase buildingSettingsBase
        ) : base(commandBuffer)
        {
            _game = game;
            _buildingSettingsBase = buildingSettingsBase;
        }

        protected override void Execute(ref BuildEnemyBuildingCommand command)
        {
            var buildingSlot = _game.GetEntityWithUid(command.BuildingSlotUid);
            var selectedBuilding = command.BuildingType;

            var settings = _buildingSettingsBase.Get(selectedBuilding);
            var price = settings.Price;
            var coins = _game.EnemyCoins.Value;
            
            _game.ReplaceEnemyCoins(coins - price);
            _game.CreateBuilding(buildingSlot.Position.Value, buildingSlot.Rotation.Value, selectedBuilding, settings, false);
        }
    }
}