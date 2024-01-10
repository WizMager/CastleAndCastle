using System.Collections.Generic;
using JCMG.EntitasRedux;

namespace Ecs.Extensions
{
    public sealed class DestroyCleaner<TContext, UEntity> : ICleanupSystem
        where UEntity : class, IEntity
        where TContext : class, IContext<UEntity>
    {
        private readonly IGroup<UEntity> _group;
        private List<UEntity> _list = new ();

        public DestroyCleaner(TContext context, IMatcher<UEntity> matcher)
        {
            _group = context.GetGroup(matcher);
        }

        public void Cleanup()
        {
            _group.GetEntities(_list);
            
            for (var i = 0; i < _list.Count; i++)
                _list[i].Destroy();
        }
    }
}