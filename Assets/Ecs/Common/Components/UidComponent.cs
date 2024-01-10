using Ecs.Extensions.UidGenerator;
using JCMG.EntitasRedux;

namespace Ecs.Common.Components
{
    [Game]
    [Input]
    public class UidComponent : IComponent
    {
        [PrimaryEntityIndex] public Uid Value;
        
        public override string ToString() => Value.ToString();
    }
}