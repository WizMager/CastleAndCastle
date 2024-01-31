using Ecs.Commands.Command;
using Ecs.Game.Extensions;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 100, nameof(EFeatures.Units))]
    public class SpawnUnitSystem : ForEachCommandUpdateSystem<SpawnUnitCommand>
    {
        private readonly GameContext _game;

        public SpawnUnitSystem(
            ICommandBuffer commandBuffer,
            GameContext game
        ) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref SpawnUnitCommand command)
        {
            var mainTarget = command.IsPlayerUnit
                ? _game.EnemyCastleEntity
                : _game.PlayerCastleEntity;
            
            var unit = _game.CreateUnit(command.Position, command.Rotation, command.UnitType, command.IsPlayerUnit);
            unit.ReplaceMainTarget(mainTarget);
        }
    }
}