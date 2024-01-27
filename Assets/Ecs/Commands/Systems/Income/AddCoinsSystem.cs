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
            var coins = _game.Coins;
            var playerCoins = coins.PlayerCoins;
            var enemyCoins = coins.EnemyCoins;

            if (isPlayerCoins)
            {
                playerCoins += command.Value;
            }
            else
            {
                enemyCoins += command.Value;
            }
            
            _game.ReplaceCoins(playerCoins, enemyCoins);
        }
    }
}