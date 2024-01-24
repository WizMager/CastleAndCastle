using Ecs.Commands;
using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Units
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 500, nameof(EFeatures.Units))]
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
            using var _ = _gameGroupUtils.GetUnits(out var units, e => e.IsInAttackRange && !e.HasTime);

            foreach (var unit in units)
            {
                var unitData = unit.UnitData.Value;
                var damage = unitData.Damage;
                var attackSpeed = unitData.AttackSpeed;
                
                unit.ReplaceTime(attackSpeed);
                
                _commandBuffer.ReceiveDamage(unit.Uid.Value, damage);
            }
        }
    }
}