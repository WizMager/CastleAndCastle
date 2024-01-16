using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Units
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 350, nameof(EFeatures.Units))]
    public class MoveToTargetSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        public MoveToTargetSystem(IGameGroupUtils gameGroupUtils)
        {
            _gameGroupUtils = gameGroupUtils;
        }

        public void Update()
        {
            using var _ = _gameGroupUtils.GetUnits(out var units, e => e.HasTarget);
            
            foreach (var unit in units)
            {
                var selfPosition = unit.Position.Value;
                var target = unit.Target.Value;

                if (!target.HasPosition)
                {
                    unit.RemoveTarget();
                    
                    continue;
                }
                
                var targetPosition = target.Position.Value;
                var attackRange = unit.UnitData.Value.AttackRange;

                var distanceSqr = (targetPosition - selfPosition).sqrMagnitude;
                
                if (distanceSqr > attackRange * attackRange)
                {
                    ChangeIsAttackRange(unit, false);
                    
                    unit.ReplaceDestinationPoint(targetPosition);
                }
                else
                {
                    ChangeIsAttackRange(unit, true);
                    
                    var destinationPoint = unit.DestinationPoint.Value;

                    if (destinationPoint == selfPosition) continue;
                    
                    unit.ReplaceDestinationPoint(selfPosition);
                }
            }
        }

        private static void ChangeIsAttackRange(GameEntity unit, bool isInAttackRange)
        {
            if (unit.IsInAttackRange == isInAttackRange) return;

            unit.IsInAttackRange = isInAttackRange;
        }
    }
}