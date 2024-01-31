using Ecs.Utils.Groups;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Units
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 510, nameof(EFeatures.Units))]
    public class MoveToMainTargetSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        public MoveToMainTargetSystem(IGameGroupUtils gameGroupUtils)
        {
            _gameGroupUtils = gameGroupUtils;
        }
        
        public void Update()
        {
            using var _ = _gameGroupUtils.GetNotDestroyedUnits(out var units, e => 
                !e.HasTarget && e.HasMainTarget && !e.IsInAttackRange);
            
            foreach (var unit in units)
            {
                var selfPosition = unit.Position.Value;
                var target = unit.MainTarget.Value;

                if (!target.HasDestinationPoint)
                {
                    unit.RemoveMainTarget();
                    
                    continue;
                }
                
                var targetPosition = target.DestinationPoint.Value;
                var attackRange = unit.UnitData.Value.AttackRange;

                var distanceSqr = (targetPosition - selfPosition).sqrMagnitude;
                
                if (distanceSqr > attackRange * attackRange)
                {
                    ChangeIsAttackRange(unit, false);
                    
                    unit.ReplaceDestinationPoint(targetPosition);
                    
                    unit.ReplaceUnitState(EUnitState.Walk);
                }
                else
                {
                    ChangeIsAttackRange(unit, true);
                    
                    unit.ReplaceDestinationPoint(selfPosition);
                    unit.ReplaceUnitState(EUnitState.Idle);
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