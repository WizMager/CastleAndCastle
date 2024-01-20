using Game.Providers.GameFieldProvider;
using Game.Services.GameLevelViewProvider;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 70, nameof(EFeatures.Initialization))]
    public class InitializeBuildingSlotsSystem : IInitializeSystem
    {
        private readonly IGameFieldProvider _gameLevelViewProvider;
        private readonly GameContext _game;

        public InitializeBuildingSlotsSystem(
            IGameFieldProvider gameLevelViewProvider, 
            GameContext game
        )
        {
            _gameLevelViewProvider = gameLevelViewProvider;
            _game = game;
        }
        
        public void Initialize()
        {
            var slots = _gameLevelViewProvider.GameField.BuildingSlotViews;
            
            for (int i = 0; i < slots.Length; i++)
            {
                var slot = slots[i];
                
                var slotEntity = _game.CreateEntity();
                slotEntity.IsBuildingSlot = true;
                
                slotEntity.AddPosition(slot.transform.position);
                slotEntity.AddRotation(slot.transform.rotation);
                slot.Link(slotEntity);

                slotEntity.IsVisible = false;
            }
        }
    }
}