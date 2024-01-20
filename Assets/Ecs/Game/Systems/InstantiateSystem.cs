using System.Collections.Generic;
using Ecs.Utils.LinkedEntityRepository;
using Ecs.Utils.SpawnService;
using Ecs.Views.Linkable;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems
{
    public class InstantiateSystem : ReactiveSystem<GameEntity>
    {
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly ISpawnService<GameEntity, IObjectLinkable> _spawnService;
        
        public InstantiateSystem(
            GameContext context,
            ILinkedEntityRepository linkedEntityRepository,
            ISpawnService<GameEntity, IObjectLinkable> spawnService
        ) : base(context)
        {
            _linkedEntityRepository = linkedEntityRepository;
            _spawnService = spawnService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Instantiate.Added());

        protected override bool Filter(GameEntity entity) => entity.IsInstantiate && !entity.IsDestroyed;

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var linkable = _spawnService.Spawn(entity);
                if (linkable == null)
                    continue;
				
                linkable.Link(entity);
                _linkedEntityRepository.Add(linkable.Hash, entity);
            }
        }
    }
}