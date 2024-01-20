using Game.Utils.Units;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    public class UnitTypeComponent : IComponent
    {
        public EUnitType Value;
    }
}