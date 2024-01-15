using Ecs.Commands;
using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Units
{
    public class AttackSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly ICommandBuffer _commandBuffer;

        public AttackSystem(
            IGameGroupUtils gameGroupUtils,
            ICommandBuffer commandBuffer
        )
        {
            _gameGroupUtils = gameGroupUtils;
            _commandBuffer = commandBuffer;
        }
        
        public void Update()
        {
            using var _ = _gameGroupUtils.GetUnits(out var units, e => e.IsInAttackRange && !e.HasAttackCooldown);

            foreach (var unit in units)
            {
                var unitData = unit.UnitData.Value;
                var damage = unitData.Damage;
                var attackSpeed = unitData.AttackSpeed;
                
                unit.ReplaceAttackCooldown(attackSpeed);
                
                _commandBuffer.ReceiveDamage(unit, damage);
            }
        }
    }
}