using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Commands.Command.Input
{
    [Command]
    public struct PointerUpCommand
    {
        public int TouchId;
    }
}