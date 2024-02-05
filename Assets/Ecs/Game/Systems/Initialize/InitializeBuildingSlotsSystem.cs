using Ecs.Extensions.UidGenerator;
using Ecs.Utils.LinkedEntityRepository;
using Ecs.Views.Linkable.Impl.Building;
using Game.Providers.GameFieldProvider;
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
        private readonly ILinkedEntityRepository _linkedEntityRepository;

        public InitializeBuildingSlotsSystem(
            IGameFieldProvider gameLevelViewProvider, 
            GameContext game,
            ILinkedEntityRepository linkedEntityRepository
        )
        {
            _gameLevelViewProvider = gameLevelViewProvider;
            _game = game;
            _linkedEntityRepository = linkedEntityRepository;
        }
        
        public void Initialize()
        {
            var playerSlots = _gameLevelViewProvider.GameField.PlayerBuildingSlotViews;
            var enemySlots = _gameLevelViewProvider.GameField.EnemyBuildingSlotViews;

            InitializeSlots(playerSlots, false, true);
            InitializeSlots(enemySlots, true, false);
        }

        private void InitializeSlots(BuildingSlotView[] slots, bool isVisible, bool isPlayer)
        {
            foreach (var slot in slots)
            {
                var slotEntity = _game.CreateEntity();
                slotEntity.IsBuildingSlot = true;

                if (isPlayer)
                {
                    slotEntity.IsPlayer = true; 
                }
                else
                {
                    slotEntity.AddUid(UidGenerator.Next());
                }
                
                slotEntity.AddPosition(slot.transform.position);
                slotEntity.AddRotation(slot.transform.rotation);
               
                slot.Link(slotEntity);

                if (!isVisible)
                {
                    slotEntity.IsVisible = false;
                }
                
                _linkedEntityRepository.Add(slot.transform.GetHashCode(), slotEntity);
            }
        }
    }
}