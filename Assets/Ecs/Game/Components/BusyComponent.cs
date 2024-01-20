using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Any)]
    [Event(EventTarget.Any, EventType.Removed)]
    public class BusyComponent : IComponent
    {
        
    }
}