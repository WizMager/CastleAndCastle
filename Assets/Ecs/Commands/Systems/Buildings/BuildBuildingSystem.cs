using Db.Buildings;
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
        private readonly IBuildingSettingsBase _buildingSettingsBase;
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly GameContext _game;

        public BuildBuildingSystem(
            ICommandBuffer commandBuffer, 
            IBuildingSettingsBase buildingSettingsBase,
            ILinkedEntityRepository linkedEntityRepository,
            GameContext game
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _buildingSettingsBase = buildingSettingsBase;
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
            
            if (!_game.HasHoveredObject)
                return;

            var hoveredObjHash = _game.HoveredObject.Hash;

            var hasSlot = _linkedEntityRepository.TryGet(hoveredObjHash, out var buildingSlot);
            
            if (!hasSlot)
                return;
            
            if (!buildingSlot.IsBuildingSlot)
                return;

            buildingSlot.IsBusy = true;

            var selectedBuilding = _game.SelectedBuilding.BuildingType;

            var settings = _buildingSettingsBase.Get(selectedBuilding);
            _game.CreateBuilding(buildingSlot.Position.Value, buildingSlot.Rotation.Value, selectedBuilding, settings, true);
            
            _commandBuffer.ExitBuildingMode();
        }
    }
}