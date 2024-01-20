using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Units
{
    [Game]
    [Event(EventTarget.Self)]
    public class DestinationPointComponent : IComponent
    {
        public Vector3 Value;
    }
}