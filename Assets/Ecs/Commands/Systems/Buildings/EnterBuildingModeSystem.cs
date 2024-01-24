using Ecs.Commands.Command;
using Ecs.Commands.Command.Buildings;
using Ecs.Utils.Groups;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using SimpleUi.Signals;
using Zenject;

namespace Ecs.Commands.Systems.Buildings
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 100, nameof(EFeatures.Building))]
    public class EnterBuildingModeSystem : ForEachCommandUpdateSystem<EnterBuildingModeCommand>
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly GameContext _game;
        private readonly SignalBus _signalBus;

        public EnterBuildingModeSystem(
            ICommandBuffer commandBuffer, 
            IGameGroupUtils gameGroupUtils,
            GameContext game,
            SignalBus signalBus
        ) : base(commandBuffer)
        {
            _gameGroupUtils = gameGroupUtils;
            _game = game;
            _signalBus = signalBus;
        }

        protected override void Execute(ref EnterBuildingModeCommand command)
        {
            using var d = _gameGroupUtils.GetBuildingSlots(out var slots, entity => !entity.IsBusy);

            foreach (var slot in slots)
            {
                slot.IsVisible = true;
            }
                
            _game.ReplaceSelectedBuilding(command.BuildingType);
                
            _signalBus.BackWindow();
        }
    }
}