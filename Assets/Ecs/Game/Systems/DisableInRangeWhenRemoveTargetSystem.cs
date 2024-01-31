using System.Collections.Generic;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Game.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 400, nameof(EFeatures.Units))]
    public class DisableInRangeWhenRemoveTargetSystem : ReactiveSystem<GameEntity>
    {
        public DisableInRangeWhenRemoveTargetSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => 
            context.CreateCollector(GameMatcher.Target.Removed());

        protected override bool Filter(GameEntity entity) => entity.IsInAttackRange && !entity.IsDestroyed;

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsInAttackRange = false;
            }
        }
    }
}