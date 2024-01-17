using Ecs.Commands.Command;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 300, nameof(EFeatures.Units))]
    public class ReceiveDamageSystem : ForEachCommandUpdateSystem<ReceiveDamageCommand>
    {
        private readonly GameContext _game;
        
        public ReceiveDamageSystem(
            ICommandBuffer commandBuffer,
            GameContext game
        ) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref ReceiveDamageCommand command)
        {
            var unitEntity = _game.GetEntityWithUid(command.TargetUid);

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