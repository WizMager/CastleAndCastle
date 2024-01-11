using System;
using UniRx.Triggers;
using UnityEngine;

namespace AnimationTriggers
{
    public class AnimatorBoolParameterTrigger : ObservableStateMachineTrigger
    {
        [SerializeField] private Parameter stateEnter;
        [SerializeField] private Parameter stateExit;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!string.IsNullOrWhiteSpace(stateEnter.name))
                animator.SetBool(stateEnter.name, stateEnter.enable);

            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!string.IsNullOrWhiteSpace(stateExit.name))
                animator.SetBool(stateExit.name, stateExit.enable);

            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        [Serializable]
        private struct Parameter
        {
            public string name;
            public bool enable;
        }
    }
}