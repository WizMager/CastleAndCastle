using Ecs.Commands.Commands;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 100, nameof(EFeatures.Building))]
    public class BuildingInputSystem : ForEachCommandUpdateSystem<MouseDownCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;


        public BuildingInputSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
        }

        protected override bool CleanUp => false;
        
        protected override void Execute(ref MouseDownCommand command)
        {
            if (command.Button == 1 && _game.HasSelectedBuilding)
            {
                _commandBuffer.Create(new ExitBuildingModeCommand());
            }
        }
    }
}