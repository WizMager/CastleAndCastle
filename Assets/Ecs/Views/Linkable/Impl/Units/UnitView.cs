﻿using Game.Utils.Units;
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
        [SerializeField] private float health;
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
            
            SelfEntity.AddUnitData(new UnitData(health, damage, attackSpeed, attackRange));
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
        }
    }
}