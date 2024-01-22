using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Buildings
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 490, nameof(EFeatures.Building))]
    public class SpawnCooldownCounterSystem : IUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        public SpawnCooldownCounterSystem(IGameGroupUtils gameGroupUtils)
        {
            _gameGroupUtils = gameGroupUtils;
        }
        
        public void Update()
        {
            using var _ = _gameGroupUtils.GetSpawnableBuilding(out var buildings, true);
            var deltaTime = Time.deltaTime;
            
            foreach (var building in buildings)
            {
                var time = building.Time.Value;
                time -= deltaTime;
                
                if (time <= 0)
                {
                    building.RemoveTime();
                }
                else
                {
                    building.ReplaceTime(time);
                }
            }
        }
    }
}