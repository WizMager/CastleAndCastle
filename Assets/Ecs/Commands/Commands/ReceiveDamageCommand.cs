using Ecs.Commands.Generator;

namespace Ecs.Commands.Commands
{
    [Command]
    public struct ReceiveDamageCommand
    {
        public GameEntity Target;
        public float Damage;
    }
}