using Db.Buildings;
using Ecs.Commands;
using Ecs.Utils.Groups;
using Ecs.Utils.Interfaces;
using JCMG.EntitasRedux.Commands;
using SimpleUi.Abstracts;
using UniRx;

namespace Game.Ui.Building
{
    public class BuildingPanelController : UiController<BuildingPanelView>, 
        IUiInitialize
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IBuildingSettingsBase _buildingSettingsBase;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;

        public BuildingPanelController(
            IGameGroupUtils gameGroupUtils, 
            IBuildingSettingsBase buildingSettingsBase,
            ICommandBuffer commandBuffer,
            GameContext game
        )
        {
            _gameGroupUtils = gameGroupUtils;
            _buildingSettingsBase = buildingSettingsBase;
            _game = game;
            _commandBuffer = commandBuffer;
        }
        
        public void Initialize()
        {
            var coins = _game.PlayerCoins.Value;
            _game.PlayerCoinsEntity.SubscribePlayerCoins(OnCoinsAdded).AddTo(View);
            
            using var slotDisposable = _gameGroupUtils.GetBuildingSlots(out var slots, entity => !entity.IsBusy);

            slots[0].SubscribeAnyBusy(OnBuildingSlotStatusChanged).AddTo(View.gameObject);
            slots[0].SubscribeAnyBusyRemoved(OnBuildingSlotStatusChanged).AddTo(View.gameObject);

            var buildings = _buildingSettingsBase.GetAll();

            foreach (var building in buildings)
            {
                var slotView = View.BuildingButtonsCollectionView.Create();

                slotView.Setup(building.Type, 
                    building.Icon, 
                    building.Name, 
                    $"{building.Price}");
                
                slotView.Btn.OnClickAsObservable()
                    .Subscribe(_ => OnBuildingButtonClick(building.Type))
                    .AddTo(slotView.gameObject);

                slotView.Btn.interactable = coins >= building.Price && slots.Count > 0;
            }
        }

        private void OnBuildingButtonClick(EBuildingType type)
        {
            _commandBuffer.EnterBuildingMode(type);
        }

        private void OnCoinsAdded(GameEntity _, int playerCoins)
        {
            foreach (var slotView in View.BuildingButtonsCollectionView)
            {
                var slotSettings = _buildingSettingsBase.Get(slotView.Id);
                
                slotView.SetIntractable(playerCoins >= slotSettings.Price);
            }
        }

        private void OnBuildingSlotStatusChanged(GameEntity entity)
        {
            using var slotDisposable = _gameGroupUtils.GetBuildingSlots(out var slots, 
                e => !e.IsBusy);

            foreach (var view in View.BuildingButtonsCollectionView)
            {
                view.SetIntractable(slots.Count > 0);
            }
        }
    }
}