using Game.Utils.Units;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Units
{
    [Game]
    public class UnitTypeComponent : IComponent
    {
        public EUnitType Value;
    }
}