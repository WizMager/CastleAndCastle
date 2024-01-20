using Game.Services.GameLevelViewProvider;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Initialize
{
    public class InitializeBuildingSlotsSystem : IInitializeSystem
    {
        private readonly IGameLevelViewProvider _gameLevelViewProvider;
        private readonly GameContext _game;

        public InitializeBuildingSlotsSystem(
            IGameLevelViewProvider gameLevelViewProvider, 
            GameContext game
        )
        {
            _gameLevelViewProvider = gameLevelViewProvider;
            _game = game;
        }
        
        public void Initialize()
        {
            var slots = _gameLevelViewProvider.LevelView.BuildingSlotViews;

            foreach (var slot in slots)
            {
                var slotEntity = _game.CreateEntity();
                slotEntity.IsBuildingSlot = true;
                
                slotEntity.AddPosition(slot.transform.position);
                slotEntity.AddRotation(slot.transform.rotation);
                slotEntity.IsVisible = false;
            }
        }
    }
}