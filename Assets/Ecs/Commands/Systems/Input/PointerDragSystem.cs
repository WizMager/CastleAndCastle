using Db.Camera;
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
        private readonly GameContext _game;
        private readonly ICameraBase _cameraBase;
        
        public PointerDragSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            ICameraBase cameraBase
        ) : base(commandBuffer)
        {
            _game = game;
            _cameraBase = cameraBase;
        }

        protected override void Execute(ref PointerDragCommand command)
        {
            var camera = _game.CameraEntity;

            if (!camera.IsCameraMove) return;
            
            var dragDelta = command.Delta;
            var moveThreshold = _cameraBase.MoveThreshold;
            
            if(Mathf.Abs(dragDelta.sqrMagnitude) <= moveThreshold * moveThreshold)
                return;
            
            var cameraTransform = camera.Transform.Value;
            var cameraPosition = cameraTransform.position;
            var moveSensitive = _cameraBase.MoveSensitive;
            
            var position = cameraTransform.right * (dragDelta.x * -moveSensitive);
            position += cameraTransform.up * (dragDelta.y * -moveSensitive);
            
            camera.ReplacePosition(cameraPosition + position * Time.deltaTime);
        }
    }
}