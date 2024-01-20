using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    public class TransformComponent : IComponent
    {
        public Transform Value;
        
        public override string ToString() => "Transform: " + (Value != null ? Value.name : "Null");
    }
}