using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Units
{
    [Game]
    [Event(EventTarget.Self)]
    public class TargetComponent : IComponent
    {
        public GameEntity Value;
    }
}