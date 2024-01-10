using JCMG.EntitasRedux;

namespace Ecs.Common.Components
{
    [Game]
    [Input]
    [Event(EventTarget.Self)]
    [Cleanup(CleanupMode.DestroyEntity)]
    public class DestroyedComponent : IComponent
    {
    }
}