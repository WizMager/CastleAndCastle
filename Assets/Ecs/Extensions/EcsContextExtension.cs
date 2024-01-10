using JCMG.EntitasRedux;
using Zenject;

namespace Ecs.Extensions
{
    public static class EcsContextExtension
    {
        public static void BindDestroyedCleanup<TContext, UEntity>(
            this DiContainer container,
            IMatcher<UEntity> matcher
        )
            where UEntity : class, IEntity
            where TContext : class, IContext<UEntity>
        {
            container.Bind<IMatcher<UEntity>>().FromInstance(matcher)
                .WhenInjectedInto<DestroyCleaner<TContext, UEntity>>();
            container.BindInterfacesAndSelfTo<DestroyCleaner<TContext, UEntity>>().AsSingle().NonLazy();
        }
    }
}