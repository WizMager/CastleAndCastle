using Db.Buildings;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Unique]
    public class SelectedBuildingComponent : IComponent
    {
        public EBuildingType BuildingType;
    }
}