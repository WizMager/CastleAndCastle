using Db.Buildings;
using Ecs.Commands.Generator;
using Ecs.Extensions.UidGenerator;

namespace Ecs.Commands.Command.Buildings
{
    [Command]
    public struct BuildEnemyBuildingCommand
    {
        public Uid BuildingSlotUid;
        public EBuildingType BuildingType;
    }
}