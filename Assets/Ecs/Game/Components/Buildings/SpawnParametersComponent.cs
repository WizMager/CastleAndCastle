using Game.Utils.Buildings;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Buildings
{
    [Game]
    public class SpawnParametersComponent : IComponent
    {
        public SpawnParameters Value;
    }
}