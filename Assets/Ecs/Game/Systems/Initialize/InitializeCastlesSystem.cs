using Db.Buildings;
using Ecs.Game.Extensions;
using Ecs.Views.Linkable.Impl.Building;
using Game.Providers.GameFieldProvider;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 45, nameof(EFeatures.Initialization))]
    public class InitializeCastlesSystem : IInitializeSystem
    {
        private readonly GameContext _game;
        private readonly DiContainer _diContainer;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IBuildingSettingsBase _buildingSettingsBase;

        public InitializeCastlesSystem(
            GameContext game,
            DiContainer diContainer,
            IGameFieldProvider gameFieldProvider,
            IBuildingSettingsBase buildingSettingsBase
        )
        {
            _game = game;
            _diContainer = diContainer;
            _gameFieldProvider = gameFieldProvider;
            _buildingSettingsBase = buildingSettingsBase;
        }
        
        public void Initialize()
        {
            var gameField = _gameFieldProvider.GameField;
            var playerCastleView = gameField.PlayerCastle;
            var enemyCastleView = gameField.EnemyCastle;
            
            InitializeCastle(playerCastleView, true);
            InitializeCastle(enemyCastleView, false);
        }

        private void InitializeCastle(CastleView castleView, bool isPlayerCastle)
        {
            _diContainer.Inject(castleView);
            var castleTransform = castleView.transform;
            var castleEntity = _game.CreateCastle(castleTransform.position, castleTransform.rotation, isPlayerCastle);
            castleView.Link(castleEntity);

            if (isPlayerCastle) return;
            
            var minimalBuildPrice = int.MaxValue;
            foreach (var building in _buildingSettingsBase.GetAll())
            {
                if (building.Price >= minimalBuildPrice) continue;

                minimalBuildPrice = building.Price;
            }
            castleEntity.AddMinimalPrice(minimalBuildPrice);
        }
    }
}