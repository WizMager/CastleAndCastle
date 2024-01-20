using Ecs.Commands.Commands;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1000, nameof(EFeatures.Input))]
    public class MouseDownCleanupSystem : ForEachCommandUpdateSystem<MouseDownCommand>
    {
        public MouseDownCleanupSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
        }
        

        protected override void Execute(ref MouseDownCommand command)
        {
            
        }
    }
}