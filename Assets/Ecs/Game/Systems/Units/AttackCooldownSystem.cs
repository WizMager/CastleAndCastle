using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Units
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 400, nameof(EFeatures.Units))]
    public class AttackCooldownSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        public AttackCooldownSystem(IGameGroupUtils gameGroupUtils)
        {
            _gameGroupUtils = gameGroupUtils;
        }
        
        public void Update()
        {
            using var _ = _gameGroupUtils.GetUnits(out var units, e => e.HasAttackCooldown);

            var deltaTime = Time.deltaTime;
            
            foreach (var unit in units)
            {
                var currentAttackCooldown = unit.AttackCooldown.Value;
                currentAttackCooldown -= deltaTime;

                if (currentAttackCooldown > 0)
                {
                    unit.ReplaceAttackCooldown(currentAttackCooldown);
                }
                else
                {
                    unit.RemoveAttackCooldown();
                }
            }
        }
    }
}