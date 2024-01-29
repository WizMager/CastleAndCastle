using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Unique]
    [Event(EventTarget.Self)]
    public class PlayerCoinsComponent : IComponent
    {
        public int Value;
    }
}