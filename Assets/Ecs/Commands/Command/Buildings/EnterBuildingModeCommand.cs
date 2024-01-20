using Db.Buildings;
using Ecs.Commands.Generator;

namespace Ecs.Commands.Command.Buildings
{
    [Command]
    public struct EnterBuildingModeCommand
    {
        public EBuildingType BuildingType;
    }
}