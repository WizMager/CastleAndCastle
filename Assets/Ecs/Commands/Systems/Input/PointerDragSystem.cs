using Ecs.Commands.Command.Input;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Commands.Systems.Input
{
    public class PointerDragSystem : ForEachCommandUpdateSystem<PointerDragCommand>
    {
        public PointerDragSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
        }

        protected override void Execute(ref PointerDragCommand command)
        {
            
        }
    }
}