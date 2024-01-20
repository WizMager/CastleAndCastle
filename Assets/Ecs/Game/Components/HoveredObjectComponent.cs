using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Unique]
    public class HoveredObjectComponent : IComponent
    {
        public int Hash;
    }
}