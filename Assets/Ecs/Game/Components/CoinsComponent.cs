using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Unique]
    [Event(EventTarget.Self)]
    public class CoinsComponent : IComponent
    {
        public int PlayerCoins;
        public int EnemyCoins;
    }
}