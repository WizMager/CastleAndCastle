using Ecs.Commands.Command.Input;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems.Input
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 170, nameof(EFeatures.Input))]
    public class PointerUpSystem : ForEachCommandUpdateSystem<PointerUpCommand>
    {
        private readonly GameContext _game;
        
        public PointerUpSystem(
            ICommandBuffer commandBuffer,
            GameContext game
        ) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref PointerUpCommand command)
        {
            var camera = _game.CameraEntity;

            camera.IsCameraMove = false;
        }
    }
}