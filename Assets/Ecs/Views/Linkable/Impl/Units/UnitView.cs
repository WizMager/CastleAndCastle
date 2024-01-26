using System;
using AnimationTriggers;
using DG.Tweening;
using Game.Utils;
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
        [SerializeField] private float liveBeforeDestroyDuration = 2;

        [Space] 
        [Header("Components")] 
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator unitAnimator;
        
        [Space] 
        [Header("Debug")] 
        [SerializeField] private bool isDrawPath;

        private IDisposable _deathBehaviour;
        
        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            base.Subscribe(entity, unsubscribe);
            
            SelfEntity.AddUnitData(new UnitData(maxHealth, damage, attackSpeed, attackRange));
            SelfEntity.AddHealth(maxHealth);
            SelfEntity.AddAggroRadius(aggroRadius);

            SelfEntity.SubscribeDestinationPoint(OnDestinationPoint).AddTo(unsubscribe);
            SelfEntity.SubscribeUnitState(OnUnitState).AddTo(unsubscribe);
            
            _deathBehaviour = unitAnimator.GetBehaviour<DeathAnimationEndTrigger>().DeathAnimationEnd.Subscribe(_ => OnDeathEnd()).AddTo(gameObject);

            Observable.EveryLateUpdate().Subscribe(_ => OnLateUpdate()).AddTo(gameObject);
        }
        
        protected override void OnPosition(GameEntity entity, Vector3 value)
        {
        }

        private void OnDestinationPoint(GameEntity entity, Vector3 value)
        {
            navMeshAgent.destination = value;
        }
        
        private void OnUnitState(GameEntity entity, EUnitState value)
        {
            switch (value)
            {
                case EUnitState.Idle:
                    unitAnimator.SetBool(AnimationKeys.Walk, false);
                    break;
                case EUnitState.Walk:
                    unitAnimator.SetBool(AnimationKeys.Walk, true);
                    break;
                case EUnitState.Death:
                    navMeshAgent.enabled = false;
                    unitAnimator.SetTrigger(AnimationKeys.Death);
                    break;
                case EUnitState.Attack:
                    unitAnimator.SetTrigger(AnimationKeys.Attack);
                    break;
            }
        }
        
        private void OnDeathEnd()
        {
            _deathBehaviour.Dispose();

            //This is second variant - just destroy GO after time. TODO: delete if not will be need
            // Observable.Timer(TimeSpan.FromSeconds(liveBeforeDestroyDuration)).Subscribe(_ =>
            // {
            //     SelfEntity.IsDestroyed = true;
            // }).AddTo(this);
            
            transform.DOMoveY(-5, liveBeforeDestroyDuration).OnComplete(() =>
            {
                SelfEntity.IsDestroyed = true;
            });
        }
        
        private void OnLateUpdate()
        {
            SelfEntity.ReplacePosition(transform.position);
            
#if UNITY_EDITOR
            if (isDrawPath)
            {
                var corners = navMeshAgent.path.corners;
            
                for (int i = 0; i < corners.Length; i++)
                {
                    if (i > corners.Length - 2) return;

                    var color = SelfEntity.IsPlayer ? Color.red : Color.green;
                
                    Debug.DrawLine(corners[i], corners[i + 1], color );
                }
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