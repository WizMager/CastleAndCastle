using Game.Providers.CameraProvider;
using Game.Services.InputService;
using Game.Utils.Raycast;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 130, nameof(EFeatures.Building))]
    public class BuildingModePointerSystem : IUpdateSystem
    {
        private readonly IRayCastProvider _rayCastProvider;
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly GameContext _game;

        public BuildingModePointerSystem(
            IRayCastProvider rayCastProvider,
            IInputService inputService,
            ICameraProvider cameraProvider,
            GameContext game
            )
        {
            _rayCastProvider = rayCastProvider;
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _game = game;
        }
        
        public void Update()
        {
            if (!_game.HasSelectedBuilding)
                return;
            var camera = _cameraProvider.GetCamera();

            var rayOrigin = camera.ScreenPointToRay(_inputService.MousePosition);
            
            // if (!_rayCastProvider.Raycast(rayOrigin.origin, 
            //         rayOrigin.direction, 
            //         out var hit, 
            //         1000, 
            //         LayerMask.NameToLayer("Building"), 
            //         QueryTriggerInteraction.Ignore))
            //     
            //     return;
            
            if (!Physics.Raycast(rayOrigin, out var hitInfo, float.MaxValue,  LayerMask.GetMask("Building"), QueryTriggerInteraction.Ignore))
            {
                return;
            }

            var buildingHash = hitInfo.transform.GetHashCode();
            
            _game.ReplaceHoveredObject(buildingHash);
            
            Debug.Log($"buildingHash: {buildingHash}, obj: {hitInfo.transform.name}");

        }
    }
}