using Db.Coins;
using Ecs.Commands.Command;
using Game.Utils.Units;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

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

            if (!unitEntity.HasTarget || !unitEntity.HasMainTarget)
            {
                unitEntity.IsInAttackRange = false;
                
                return;
            }
            Debug.Log($"targ: {unitEntity.HasTarget}");
            if (unitEntity.HasTarget)
            {
                DealDamageTarget(unitEntity, command.Damage);
            }
            else
            {
                DealDamageMainTarget(unitEntity, command.Damage);
            }
        }

        private void DealDamageTarget(GameEntity unitEntity, float damage)
        {
            Debug.Log($"dam targ");
            var targetEntity = unitEntity.Target.Value;

            if (!targetEntity.HasHealth)
            {
                unitEntity.RemoveTarget();
                
                return;
            }
            
            var health = targetEntity.Health.Value;
            health -= damage;

            if (health <= 0)
            {
                var isPlayer = targetEntity.IsPlayer;
                var unitType = targetEntity.UnitType.Value;
                var dropCoins = _dropCoinsFromUnitsBase.GetCoinsForUnitType(unitType);

                if (isPlayer)
                {
                    var enemyCoins = _game.EnemyCoins.Value;
                    enemyCoins += dropCoins;
                    
                    _game.ReplaceEnemyCoins(enemyCoins);
                }
                else
                {
                    var playerCoins = _game.PlayerCoins.Value;
                    playerCoins += dropCoins;
                    
                    _game.ReplacePlayerCoins(playerCoins);
                }
                
                targetEntity.IsDead = true;
                targetEntity.ReplaceUnitState(EUnitState.Death);
                unitEntity.RemoveTarget();
            }
            else
            {
                targetEntity.ReplaceHealth(health);
            }
        }
        
        private void DealDamageMainTarget(GameEntity unitEntity, float damage)
        {
            Debug.Log($"dam cast");
            var targetEntity = unitEntity.MainTarget.Value;

            if (!targetEntity.HasHealth)
            {
                unitEntity.RemoveMainTarget();
                
                return;
            }
            
            var health = targetEntity.Health.Value;
            health -= damage;

            if (health <= 0)
            {
                var isPlayer = targetEntity.IsPlayerCastle;
                
                //TODO: send command for lose or win after check which castle is it was
                
                unitEntity.RemoveMainTarget();
            }
            else
            {
                targetEntity.ReplaceHealth(health);
            }
        }
    }
}