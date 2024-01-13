using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using JCMG.EntitasRedux.Core.View.Impls;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl
{
    public class ObjectView : LinkableView, IObjectLinkable
    {
        protected GameEntity SelfEntity;
        
        public Transform Transform => transform;
        
        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            SelfEntity = (GameEntity) entity;
            
            SelfEntity.SubscribePosition(OnPosition).AddTo(unsubscribe);
            SelfEntity.SubscribeRotation(OnRotation).AddTo(unsubscribe);
        }

        protected virtual void OnPosition(GameEntity entity, Vector3 value)
        {
            transform.position = value;
        }

        protected virtual void OnRotation(GameEntity entity, Quaternion value)
        {
            transform.rotation = value;
        }
        
        protected override void OnClear()
        {
            Destroy(gameObject);
        }
    }
}