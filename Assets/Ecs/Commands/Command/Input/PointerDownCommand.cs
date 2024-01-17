using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Commands.Command.Input
{
    [Command]
    public struct PointerDownCommand
    {
        public int TouchId;
        public Vector3 Position;
    }
}