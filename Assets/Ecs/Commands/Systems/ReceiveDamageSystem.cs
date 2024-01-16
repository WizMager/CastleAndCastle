using Ecs.Commands.Commands;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 300, nameof(EFeatures.Units))]
    public class ReceiveDamageSystem : ForEachCommandUpdateSystem<ReceiveDamageCommand>
    {
        public ReceiveDamageSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
        }

        protected override void Execute(ref ReceiveDamageCommand command)
        {
            var unitEntity = command.Target;

            if (!unitEntity.HasTarget)
            {
                unitEntity.IsInAttackRange = false;
                
                return;
            }
            
            var targetEntity = unitEntity.Target.Value;

            if (!targetEntity.HasHealth)
            {
                unitEntity.RemoveTarget();
                
                return;
            }
            var health = targetEntity.Health.Value;
            health -= command.Damage;

            if (health <= 0)
            {
                targetEntity.IsDestroyed = true;
                unitEntity.RemoveTarget();
            }
            else
            {
                targetEntity.ReplaceHealth(health);
            }
        }
    }
}