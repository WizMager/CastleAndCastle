using Ecs.Commands.Command.Income;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems.Income
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 130, nameof(EFeatures.Coins))]
    public class AddCoinsSystem : ForEachCommandUpdateSystem<AddCoinsCommand>
    {
        private readonly GameContext _game;

        public AddCoinsSystem(
            ICommandBuffer commandBuffer, 
            GameContext game
        ) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref AddCoinsCommand command)
        {
            var isPlayerCoins = command.IsPlayer;

            if (isPlayerCoins)
            {
                var playerCoins = _game.PlayerCoins.Value;
                playerCoins += command.Value;
                
                _game.ReplacePlayerCoins(playerCoins);
            }
            else
            {
                var enemyCoins = _game.EnemyCoins.Value;
                enemyCoins += command.Value;
                
                _game.ReplaceEnemyCoins(enemyCoins);
            }
        }
    }
}