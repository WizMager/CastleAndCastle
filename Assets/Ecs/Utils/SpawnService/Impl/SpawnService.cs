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
            if (!entity.HasPrefab) 
                throw new Exception($"[{typeof(SpawnService)}]: Can't instantiate entity: " + entity);
            
            var prefabName = entity.Prefab.Value;
            var position = entity.HasPosition ? entity.Position.Value : Vector3.zero;
            
            return _prefabPoolService.Spawn(prefabName, position, Quaternion.identity, out var linkable)
                ? linkable
                : InstantiateLinkable(position, _prefabsBase.Get(prefabName));
        }

        private IObjectLinkable InstantiateLinkable(Vector3 position, GameObject prefab)
        {
            var gameObject = _container.InstantiatePrefab(prefab, position, Quaternion.identity, null);
            return gameObject.GetComponent<IObjectLinkable>();
        }
    }
}