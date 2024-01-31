using Db.Buildings;
using Ecs.Game.Extensions;
using Ecs.Utils.Groups;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Buildings
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 500, nameof(EFeatures.Building))]
    public class SpawnUnitsSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly GameContext _game;

        public SpawnUnitsSystem(
            IGameGroupUtils gameGroupUtils,
            GameContext game
        )
        {
            _gameGroupUtils = gameGroupUtils;
            _game = game;
        }
        
        public void Update()
        {
            using var _ = _gameGroupUtils.GetSpawnableBuilding(out var buildings, false);

            foreach (var building in buildings)
            {
                var parameters = building.SpawnParameters.Value;
                var buildingType = building.BuildingType.Value;
                var isPlayerBuilding = building.IsPlayer;

                var unitType = buildingType switch
                {
                    EBuildingType.Farm => EUnitType.MeleeUnit,
                    EBuildingType.House => EUnitType.RangeUnit
                };
                
                var unit = _game.CreateUnit(parameters.SpawnPosition, parameters.SpawnRotation, unitType, isPlayerBuilding);
                var mainTarget = isPlayerBuilding
                    ? _game.EnemyCastleEntity
                    : _game.PlayerCastleEntity;
                unit.ReplaceMainTarget(mainTarget);
                
                building.AddTime(parameters.SpawnCooldown);
            }
        }
    }
}