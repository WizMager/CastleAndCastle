using Ecs.Commands.Command.Input;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Commands.Systems.Input
{
    public class PointerUpSystem : ForEachCommandUpdateSystem<PointerUpCommand>
    {
        public PointerUpSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
        }

        protected override void Execute(ref PointerUpCommand command)
        {
            
        }
    }
}