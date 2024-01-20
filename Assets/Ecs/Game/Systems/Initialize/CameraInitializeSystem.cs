using Ecs.Game.Extensions;
using Ecs.Views.Linkable.Impl;
using Game.Providers.CameraProvider;
using Game.Providers.GameFieldProvider;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 60, nameof(EFeatures.Initialization))]
    public class CameraInitializeSystem : IInitializeSystem
    {
        private readonly DiContainer _diContainer;
        private readonly ICameraProvider _cameraProvider;
        private readonly GameContext _game;
        private readonly IGameFieldProvider _gameFieldProvider;

        public CameraInitializeSystem(
            DiContainer diContainer,
            ICameraProvider cameraProvider,
            GameContext game,
            IGameFieldProvider gameFieldProvider
        )
        {
            _diContainer = diContainer;
            _cameraProvider = cameraProvider;
            _game = game;
            _gameFieldProvider = gameFieldProvider;
        }
        
        public void Initialize()
        {
            var camera = _cameraProvider.GetCamera();
            var cameraView = camera.GetComponent<CameraView>();
            var cameraTransform = cameraView.transform;
            var levelCameraPosition = _gameFieldProvider.GameField.StartCameraPosition;
            
            _diContainer.Inject(cameraView);
            cameraTransform.SetPositionAndRotation(levelCameraPosition.position, levelCameraPosition.rotation);

            var cameraEntity = _game.CreateCamera(cameraTransform);
            cameraView.Link(cameraEntity);
        }
    }
}