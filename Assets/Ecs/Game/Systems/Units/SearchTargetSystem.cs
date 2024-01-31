using System.Collections.Generic;
using Ecs.Utils.Groups;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Units
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 300, nameof(EFeatures.Units))]
    public class SearchTargetSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        public SearchTargetSystem(IGameGroupUtils gameGroupUtils)
        {
            _gameGroupUtils = gameGroupUtils;
        }
        
        public void Update()
        {
            CheckMeleeUnits(_gameGroupUtils, true);
            CheckMeleeUnits(_gameGroupUtils, false);
            
            CheckRangeUnits(_gameGroupUtils, true);
            CheckRangeUnits(_gameGroupUtils, false);
        }
        
        private void CheckMeleeUnits(IGameGroupUtils gameGroupUtils, bool isPlayerUnit)
        {
            using var group1 = gameGroupUtils.GetOwnerUnitsWithType(out var enemyUnits, isPlayerUnit, EUnitType.MeleeUnit, e => !e.HasTarget);
            using var group2 = gameGroupUtils.GetOwnerUnitsWithType(out var notTargetedPlayer, !isPlayerUnit, EUnitType.MeleeUnit, e => !e.IsInTarget);

            if (notTargetedPlayer.Count == 0)
            {
                using var group3 = gameGroupUtils.GetOwnerUnits(out var targetedPlayerUnits, !isPlayerUnit);
                
                CheckAggroRadius(enemyUnits, targetedPlayerUnits);
            }
            else
            {
                CheckAggroRadius(enemyUnits, notTargetedPlayer);
            }
        }
        
        private void CheckRangeUnits(IGameGroupUtils gameGroupUtils, bool isPlayerUnit)
        {
            using var group1 = gameGroupUtils.GetOwnerUnitsWithType(out var seekerUnits, isPlayerUnit, EUnitType.RangeUnit, e => !e.HasTarget);
            using var group2 = gameGroupUtils.GetOwnerUnits(out var checkedUnits, !isPlayerUnit);

            CheckAggroRadius(seekerUnits, checkedUnits);
        }

        private void CheckAggroRadius(List<GameEntity> seekerUnits, IReadOnlyList<GameEntity> checkedUnits)
        {
            foreach (var seekerUnit in seekerUnits)
            {
                var aggroRadius = seekerUnit.AggroRadius.Value;
                var playerPosition = seekerUnit.Position.Value;
                var enemiesInRange = new Dictionary<int, float>();

                for (var i = 0; i < checkedUnits.Count; i++)
                {
                    var enemyPosition = checkedUnits[i].Position.Value;
                    var distanceToEnemy = (enemyPosition - playerPosition).sqrMagnitude;
                    
                    if (distanceToEnemy > aggroRadius * aggroRadius) continue;
                    
                    enemiesInRange.Add(i, distanceToEnemy);
                }
                
                if (enemiesInRange.Count == 0) continue;
                
                var nearestUnitIndex = CheckNearestUnit(enemiesInRange);
                var nearestUnit = checkedUnits[nearestUnitIndex];
                nearestUnit.IsInTarget = true;
                seekerUnit.ReplaceTarget(nearestUnit);
                seekerUnit.ReplaceUnitState(EUnitState.Walk);
            }
        }
        
        private static int CheckNearestUnit(IReadOnlyDictionary<int, float> unitIndexesWithRange)
        {
            var nearestUnitIndex = -1;
            var range = 10000f;
            
            foreach (var unit in unitIndexesWithRange)
            {
                if (unit.Value > range) continue;

                nearestUnitIndex = unit.Key;
                range = unit.Value;
            }

            return nearestUnitIndex;
        }
    }
}