using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Buildings
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 110, nameof(EFeatures.Building))]
    public class EnemyBuildTimeSystem : IUpdateSystem
    {
        private readonly GameContext _game;

        public EnemyBuildTimeSystem(GameContext game)
        {
            _game = game;
        }
        
        public void Update()
        {
            var enemyCastle = _game.EnemyCastleEntity;

            if (!enemyCastle.HasTime) return;
            
            var time = enemyCastle.Time.Value;
            
            time -= Time.deltaTime;

            if (time <= 0)
            {
                enemyCastle.RemoveTime();
            }
            else
            {
                enemyCastle.ReplaceTime(time);
            }
        }
    }
}