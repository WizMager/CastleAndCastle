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

            if (!camera.HasCameraMove) return;

            var startPosition = camera.CameraMove.StartTouchPosition;
            var dragDelta = command.Delta;
            
            if(Mathf.Abs(dragDelta.sqrMagnitude) <= DRAG_THRESHOLD)
                return;
            
            var pinPosition = startPosition;
            pinPosition = NormalizeScreenPosition(pinPosition);


            var currentPosition = command.Delta;
            currentPosition = NormalizeScreenPosition(currentPosition);

            var diff = pinPosition - currentPosition;
        }
        
        private Vector2 NormalizeScreenPosition(Vector2 screenPosition)
        {
            return new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
        }
    }
}