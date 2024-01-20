using Ecs.Commands.Command;
using Ecs.Commands.Command.Buildings;
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
            
            var building = _game.CreateEntity();
            building.AddPrefab(selectedBuilding.ToString());
            building.AddPosition(buildingSlot.Position.Value);
            building.AddRotation(buildingSlot.Rotation.Value);
        
            building.IsInstantiate = true;
            
            _commandBuffer.ExitBuildingMode();
        }
    }
}