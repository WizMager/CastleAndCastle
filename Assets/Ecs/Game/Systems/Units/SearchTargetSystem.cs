using System.Collections.Generic;
using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Systems.Units
{
    public class SearchTargetSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        public SearchTargetSystem(IGameGroupUtils gameGroupUtils)
        {
            _gameGroupUtils = gameGroupUtils;
        }
        
        public void Update()
        {
            using var group1 = _gameGroupUtils.GetOwnerUnits(out var playerUnits, true, e => !e.HasTarget);
            using var group2 = _gameGroupUtils.GetOwnerUnits(out var enemyUnits, false, e => !e.HasTarget);

            CheckUnitsTarget(playerUnits, enemyUnits);
            CheckUnitsTarget(enemyUnits, playerUnits);
        }

        private static void CheckUnitsTarget(List<GameEntity> checkUnits, IReadOnlyList<GameEntity> targetUnits)
        {
            foreach (var checkUnit in checkUnits)
            {
                var aggroRadius = checkUnit.AggroRadius.Value;
                var playerPosition = checkUnit.Position.Value;
                var enemiesInRange = new Dictionary<int, float>();

                for (var i = 0; i < targetUnits.Count; i++)
                {
                    var enemyPosition = targetUnits[i].Position.Value;
                    var distanceToEnemy = (enemyPosition - playerPosition).sqrMagnitude;
                    
                    if (distanceToEnemy > aggroRadius * aggroRadius) continue;
                    
                    enemiesInRange.Add(i, distanceToEnemy);
                }
                
                if (enemiesInRange.Count == 0) continue;

                var nearestUnitIndex = CheckNearestUnit(enemiesInRange);
                var nearestUnit = targetUnits[nearestUnitIndex];
                
                checkUnit.ReplaceTarget(nearestUnit);
            }
        }
        
        private static int CheckNearestUnit(IReadOnlyDictionary<int, float> unitIndexesWithRange)
        {
            var nearestUnitIndex = 0;
            var range = unitIndexesWithRange[0];

            for (var i = 1; i < unitIndexesWithRange.Count; i++)
            {
                if (unitIndexesWithRange[i] > range) continue;

                nearestUnitIndex = i;
                range = unitIndexesWithRange[i];
            }

            return nearestUnitIndex;
        }
    }
}