using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UniRx;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Building
{
    public class BuildingSlotView : ObjectView
    {
        [SerializeField] private GameObject slotGo;

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            var slotEntity = (GameEntity)entity;
            slotEntity.SubscribeVisible(OnSlotVisibleAdded).AddTo(unsubscribe);
            slotEntity.SubscribeVisibleRemoved(OnSlotVisibleRemoved).AddTo(unsubscribe);
            
            base.Subscribe(entity, unsubscribe);
        }

        private void OnSlotVisibleAdded(GameEntity entity)
        {
            slotGo.SetActive(true);
        }
        
        private void OnSlotVisibleRemoved(GameEntity entity)
        {
            slotGo.SetActive(false);
        }
    }
}