using Ecs.Utils.Groups;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Units
{
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
                var targetPosition = target.Position.Value;
                var attackRange = unit.UnitData.Value.AttackRange;

                var distanceSqr = (targetPosition - selfPosition).sqrMagnitude;
                
                if (distanceSqr > attackRange * attackRange)
                {
                    unit.ReplaceDestinationPoint(targetPosition);
                }
                else
                {
                    var destinationPoint = unit.DestinationPoint.Value;

                    if (destinationPoint == selfPosition) continue;
                    
                    unit.ReplaceDestinationPoint(selfPosition);
                }
            }
        }
    }
}