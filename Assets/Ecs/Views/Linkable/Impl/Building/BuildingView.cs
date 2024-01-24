using Game.Utils.Buildings;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Building
{
    public class BuildingView : ObjectView
    {
        [SerializeField] private float spawnCooldown;
        [SerializeField] private Transform spawnPosition;

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            base.Subscribe(entity, unsubscribe);
            
            SelfEntity.AddSpawnParameters(new SpawnParameters(spawnCooldown, spawnPosition.position, spawnPosition.rotation));
            SelfEntity.AddTime(spawnCooldown);
        }
    }
}