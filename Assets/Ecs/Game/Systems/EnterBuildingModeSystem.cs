using System;
using Ecs.Commands;
using Ecs.Utils.Groups;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems
{
    public class EnterBuildingModeSystem : CommandSystem<EnterBuildingModeCommand>
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly GameContext _game;

        public EnterBuildingModeSystem(
            ICommandBuffer commandBuffer, 
            IGameGroupUtils gameGroupUtils,
            GameContext game
        ) : base(commandBuffer)
        {
            _gameGroupUtils = gameGroupUtils;
            _game = game;
        }

        protected override void Execute(Span<EnterBuildingModeCommand> commands)
        {
            foreach (var command in commands)
            {
                using var d = _gameGroupUtils.GetBuildingSlots(out var slots, entity => !entity.IsBusy);

                foreach (var slot in slots)
                {
                    slot.IsVisible = true;
                }
                
                _game.ReplaceSelectedBuilding(command.BuildingType);
            }
        }
    }
}