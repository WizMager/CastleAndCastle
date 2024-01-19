using Ecs.Commands.Command.Input;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Commands.Systems.Input
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 150, nameof(EFeatures.Input))]
    public class PointerDragSystem : ForEachCommandUpdateSystem<PointerDragCommand>
    {
        private const float DRAG_THRESHOLD = 0.0001f;
        private const float SENSITIVE = 0.7f;

        private readonly GameContext _game;
        
        public PointerDragSystem(
            ICommandBuffer commandBuffer,
            GameContext game
        ) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref PointerDragCommand command)
        {
            var camera = _game.CameraEntity;

            if (!camera.IsCameraMove) return;
            
            var dragDelta = command.Delta;
            
            if(Mathf.Abs(dragDelta.sqrMagnitude) <= DRAG_THRESHOLD)
                return;
            
            var moveDelta = command.Delta;
            moveDelta *= SENSITIVE;

            var result = Convert(camera, moveDelta);
            
            Debug.Log($"DRAG: {moveDelta} | {result}");
            var cameraPosition = camera.Position.Value;
            cameraPosition.x += result.x;
            cameraPosition.z -= result.y;
            camera.ReplacePosition(cameraPosition);
        }

        private Vector3 Convert(IRotationEntity camera, Vector3 moveDelta)
        {
            var cameraRotation = camera.Rotation.Value;
            var angle = cameraRotation.eulerAngles.y;
            return Quaternion.Euler(0, angle, 0) * moveDelta;
        }
    }
}