using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Commands.Command.Input
{
    [Command]
    public struct PointerDragCommand
    {
        public int TouchId;
        public Vector3 Delta;
    }
}