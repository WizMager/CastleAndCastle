using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AnimationTriggers
{
    public class DeathAnimationEndTrigger : ObservableStateMachineTrigger
    {
        private readonly ReactiveCommand _deathAnimationEndCommand = new ();

        public IObservable<Unit> DeathAnimationEnd => _deathAnimationEndCommand;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var animationLength = stateInfo.normalizedTime;
            
            if (animationLength is >= 0.95f and <= 0.96f)
            {
                _deathAnimationEndCommand.Execute();
            }
            
            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }
    }
}