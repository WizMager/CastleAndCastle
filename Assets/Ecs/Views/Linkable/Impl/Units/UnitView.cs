using Game.Utils.Units;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Units
{
    public class UnitView : ObjectView
    {
        [SerializeField] private float health;
        [SerializeField] private float damage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackRange;
        
        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            base.Subscribe(entity, unsubscribe);
            
            SelfEntity.AddUnitData(new UnitData(health, damage, attackSpeed, attackRange));
        }
    }
}