using Ecs.Commands.Command.Input;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Commands.Systems.Input
{
    public class PointerDownSystem : ForEachCommandUpdateSystem<PointerDownCommand>
    {
        public PointerDownSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
        }

        protected override void Execute(ref PointerDownCommand command)
        {
            
        }
    }
}