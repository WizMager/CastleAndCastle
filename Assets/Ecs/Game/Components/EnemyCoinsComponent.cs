using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Unique]
    public class EnemyCoinsComponent : IComponent
    {
        public int Value;
    }
}