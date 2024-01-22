using Db.Buildings;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Buildings
{
    [Game]
    public class BuildingTypeComponent : IComponent
    {
        public EBuildingType Value;
    }
}