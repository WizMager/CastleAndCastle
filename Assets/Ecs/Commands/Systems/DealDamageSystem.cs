using Db.Coins;
using Ecs.Commands.Command;
using Game.Utils.Units;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Commands.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 300, nameof(EFeatures.Units))]
    public class DealDamageSystem : ForEachCommandUpdateSystem<DealDamageCommand>
    {
        private readonly GameContext _game;
        private readonly IDropCoinsFromUnitsBase _dropCoinsFromUnitsBase;
        
        public DealDamageSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IDropCoinsFromUnitsBase dropCoinsFromUnitsBase
        ) : base(commandBuffer)
        {
            _game = game;
            _dropCoinsFromUnitsBase = dropCoinsFromUnitsBase;
        }

        protected override void Execute(ref DealDamageCommand command)
        {
            var unitEntity = _game.GetEntityWithUid(command.TargetUid);

            if (!unitEntity.HasTarget)
            {
                unitEntity.IsInAttackRange = false;
                
                return;
            }
            
            var targetEntity = unitEntity.Target.Value;

            if (!targetEntity.HasHealth)
            {
                unitEntity.RemoveTarget();
                
                return;
            }
            
            var health = targetEntity.Health.Value;
            health -= command.Damage;

            if (health <= 0)
            {
                var isPlayer = targetEntity.IsPlayer;
                var unitType = targetEntity.UnitType.Value;
                var dropCoins = _dropCoinsFromUnitsBase.GetCoinsForUnitType(unitType);
                
                var coins = _game.Coins;
                var playerCoins = coins.PlayerCoins;
                var enemyCoins = coins.EnemyCoins;

                if (isPlayer)
                {
                    enemyCoins += dropCoins;
                }
                else
                {
                    playerCoins += dropCoins;
                }
                
                _game.ReplaceCoins(playerCoins, enemyCoins);
                targetEntity.IsDead = true;
                targetEntity.ReplaceUnitState(EUnitState.Death);
                unitEntity.RemoveTarget();
            }
            else
            {
                targetEntity.ReplaceHealth(health);
            }
        }
    }
}