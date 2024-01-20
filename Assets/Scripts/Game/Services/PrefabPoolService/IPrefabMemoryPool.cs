using Ecs.Views.Linkable;
using Zenject;

namespace Game.Services.PrefabPoolService
{
    public interface IPrefabMemoryPool : IMemoryPool<IObjectLinkable>
    {
        string Name { get; }
    }
}