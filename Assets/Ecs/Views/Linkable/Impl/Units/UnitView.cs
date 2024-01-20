using System;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Ecs.Views.Linkable.Impl.Units
{
    public class UnitView : ObjectView
    {
        [Header("Unit parameters:")]
        [SerializeField] private float maxHealth;
        [SerializeField] private float damage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackRange;
        [SerializeField] private float aggroRadius;

        [Space] 
        [Header("Components")] 
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            base.Subscribe(entity, unsubscribe);
            
            SelfEntity.AddUnitData(new UnitData(maxHealth, damage, attackSpeed, attackRange));
            SelfEntity.AddHealth(maxHealth);
            SelfEntity.AddAggroRadius(aggroRadius);

            SelfEntity.SubscribeDestinationPoint(OnDestinationPoint).AddTo(unsubscribe);

            Observable.EveryLateUpdate().Subscribe(_ => OnLateUpdate()).AddTo(gameObject);
        }

        protected override void OnPosition(GameEntity entity, Vector3 value)
        {
        }

        private void OnDestinationPoint(GameEntity entity, Vector3 value)
        {
            navMeshAgent.destination = value;
        }
        
        private void OnLateUpdate()
        {
            SelfEntity.ReplacePosition(transform.position);
            
#if UNITY_EDITOR
            var corners = navMeshAgent.path.corners;
            
            for (int i = 0; i < corners.Length; i++)
            {
                if (i > corners.Length - 2) return;

                var color = SelfEntity.IsPlayerUnit ? Color.red : Color.green;
                
                Debug.DrawLine(corners[i], corners[i + 1], color );
            }
#endif
        }

        protected override void OnClear()
        {
            if (SelfEntity.HasTarget)
            {
                var targetEntity = SelfEntity.Target.Value;
                targetEntity.IsInTarget = false;
            }
            
            base.OnClear();
        }
    }
}