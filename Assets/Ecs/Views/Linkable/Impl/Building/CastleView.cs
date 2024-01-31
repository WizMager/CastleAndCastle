using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Building
{
    public class CastleView : ObjectView
    {
        [SerializeField] private float startHealth = 100f;
        [SerializeField] private Transform destinationPosition;

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            base.Subscribe(entity, unsubscribe);
            
            SelfEntity.AddHealth(startHealth);
            SelfEntity.AddDestinationPoint(destinationPosition.position);
        }
    }
}