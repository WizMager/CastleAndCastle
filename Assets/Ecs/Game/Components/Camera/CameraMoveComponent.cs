using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Camera
{
    [Game]
    public class CameraMoveComponent : IComponent
    {
        public Vector3 StartTouchPosition;
    }
}