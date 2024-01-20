using System;
using Ecs.Commands.Commands;
using Ecs.Utils.Groups;
using Game.Ui.Windows;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using SimpleUi.Signals;
using Zenject;

namespace Ecs.Commands.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 120, nameof(EFeatures.Building))]
    public class ExitBuildingModeSystem : ForEachCommandUpdateSystem<MouseDownCommand>
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly SignalBus _signalBus;
        private readonly GameContext _game;

        public ExitBuildingModeSystem(
            ICommandBuffer commandBuffer,
            IGameGroupUtils gameGroupUtils,
            SignalBus signalBus,
            GameContext game) : base(commandBuffer)
        {
            _gameGroupUtils = gameGroupUtils;
            _signalBus = signalBus;
            _game = game;
        }

        protected override void Execute(ref MouseDownCommand command)
        {
            if (command.Button != 1)
                return;
            
            if (!_game.HasSelectedBuilding)
                return;
            
            _game.RemoveSelectedBuilding();
            
            _signalBus.OpenWindow<GameHudWindow>();
            
            using var d = _gameGroupUtils.GetBuildingSlots(out var slots, entity => !entity.IsBusy);

            foreach (var slot in slots)
            {
                slot.IsVisible = false;
            }
        }
    }
}