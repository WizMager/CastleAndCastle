using Db.Buildings;

namespace Ecs.Commands
{
    public readonly struct EnterBuildingModeCommand
    {
        public readonly EBuildingType BuildingType;

        public EnterBuildingModeCommand(EBuildingType buildingType)
        {
            BuildingType = buildingType;
        }
    }
}