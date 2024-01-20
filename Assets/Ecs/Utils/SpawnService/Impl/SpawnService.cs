using System;
using Db.Prefabs;
using Ecs.Views.Linkable;
using Game.Services.PrefabPoolService;
using UnityEngine;
using Zenject;

namespace Ecs.Utils.SpawnService.Impl
{
    public class SpawnService : ISpawnService<GameEntity, IObjectLinkable>
    {
        private readonly DiContainer _container;
        private readonly IPrefabsBase _prefabsBase;
        private readonly IPrefabPoolService _prefabPoolService;

        public SpawnService(
            DiContainer container,
            IPrefabsBase prefabsBase,
            IPrefabPoolService prefabPoolService
        )
        {
            _container = container;
            _prefabsBase = prefabsBase;
            _prefabPoolService = prefabPoolService;
        }

        public IObjectLinkable Spawn(GameEntity entity)
        {
            // if (!entity.HasUnitType) 
            //     throw new Exception($"[{typeof(SpawnService)}]: Can't instantiate entity: " + entity);
            
            var prefabName = entity.Prefab.Value.ToString();
            var position = entity.HasPosition ? entity.Position.Value : Vector3.zero;
            var rotation = entity.HasRotation ? entity.Rotation.Value : Quaternion.identity;
            
            return _prefabPoolService.Spawn(prefabName, position,rotation, out var linkable)
                ? linkable
                : InstantiateLinkable(position, rotation, _prefabsBase.Get(prefabName));
        }

        private IObjectLinkable InstantiateLinkable(Vector3 position, Quaternion rotation, GameObject prefab)
        {
            var gameObject = _container.InstantiatePrefab(prefab, position, rotation, null);
            return gameObject.GetComponent<IObjectLinkable>();
        }
    }
}