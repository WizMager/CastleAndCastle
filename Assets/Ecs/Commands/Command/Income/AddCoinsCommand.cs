using Ecs.Commands.Generator;

namespace Ecs.Commands.Command.Income
{
    [Command]
    public struct AddCoinsCommand
    {
        public int Value;
        public bool IsPlayer;
    }
}