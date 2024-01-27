using Ecs.Commands.Generator;
using Ecs.Extensions.UidGenerator;

namespace Ecs.Commands.Command
{
    [Command]
    public struct DealDamageCommand
    {
        public Uid TargetUid;
        public float Damage;
    }
}