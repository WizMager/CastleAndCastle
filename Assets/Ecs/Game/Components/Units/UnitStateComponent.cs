using Game.Utils.Units;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Units
{
    [Game]
    [Event(EventTarget.Self)]
    public class UnitStateComponent : IComponent
    {
        public EUnitState Value;
    }
}