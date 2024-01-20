using Ecs.Utils.Groups;
using JCMG.EntitasRedux;
using UniRx;
using Zenject;

namespace Game.Services.BuildingSlot.Impl
{
    public class BuildingSlotService : IBuildingSlotService, IInitializable
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly GameContext _game;

        private ReactiveCommand _slotReleased;

        public BuildingSlotService(IGameGroupUtils gameGroupUtils, GameContext game)
        {
            _gameGroupUtils = gameGroupUtils;
            _game = game;
        }

        public IReactiveCommand<Unit> SlotReleased => _slotReleased;
        
        public void Initialize()
        {
            _game.OnEntityCreated += OnEntityCreated;
        }

        private void OnEntityCreated(
            IContext context, 
            IEntity entity
        )
        {
            // var gameEntity = (GameEntity)entity;
            //
            // if (!gameEntity.IsBuildingSlot)
            //     return;
            //
            // gameEntity.subs
        }
    }
}