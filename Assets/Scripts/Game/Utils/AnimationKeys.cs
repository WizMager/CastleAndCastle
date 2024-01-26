using UnityEngine;

namespace Game.Utils
{
    public static class AnimationKeys
    {
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Death = Animator.StringToHash("Death");
    }
}