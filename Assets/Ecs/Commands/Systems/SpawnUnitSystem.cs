using Ecs.Commands.Command;
using Ecs.Game.Extensions;
using Game.Providers.GameFieldProvider;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 100, nameof(EFeatures.Units))]
    public class SpawnUnitSystem : ForEachCommandUpdateSystem<SpawnUnitCommand>
    {
        private readonly GameContext _game;
        private readonly IGameFieldProvider _gameFieldProvider;

        public SpawnUnitSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IGameFieldProvider gameFieldProvider
        ) : base(commandBuffer)
        {
            _game = game;
            _gameFieldProvider = gameFieldProvider;
        }

        protected override void Execute(ref SpawnUnitCommand command)
        {
            var destinationPoint = command.IsPlayerUnit
                ? _gameFieldProvider.GameField.EnemyCastlePosition
                : _gameFieldProvider.GameField.PlayerCastlePosition;
            
            var unit = _game.CreateUnit(command.Position, command.Rotation, command.UnitType, command.IsPlayerUnit);
            unit.ReplaceDestinationPoint(destinationPoint);
        }
    }
}