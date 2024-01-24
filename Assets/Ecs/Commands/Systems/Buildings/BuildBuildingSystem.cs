using Ecs.Commands.Command;
using Ecs.Commands.Command.Buildings;
using Ecs.Game.Extensions;
using Ecs.Utils.LinkedEntityRepository;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems.Buildings
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 130, nameof(EFeatures.Building))]
    public class BuildBuildingSystem : ForEachCommandUpdateSystem<MouseDownCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly ILinkedEntityRepository _linkedEntityRepository;

        public BuildBuildingSystem(
            ICommandBuffer commandBuffer, 
            GameContext game, 
            ILinkedEntityRepository linkedEntityRepository
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _linkedEntityRepository = linkedEntityRepository;
        }

        protected override bool CleanUp => false;

        protected override void Execute(ref MouseDownCommand command)
        {
            if (!_game.HasSelectedBuilding)
                return;
            
            if (command.Button != 0)
                return;

            var hoveredObjHash = _game.HoveredObject.Hash;

            _linkedEntityRepository.TryGet(hoveredObjHash, out var buildingSlot);
            
            if (!buildingSlot.IsBuildingSlot)
                return;

            buildingSlot.IsBusy = true;

            var selectedBuilding = _game.SelectedBuilding.BuildingType;

            _game.CreateBuilding(buildingSlot.Position.Value, buildingSlot.Rotation.Value, selectedBuilding, true);
            
            _commandBuffer.ExitBuildingMode();
        }
    }
}