using Ecs.Commands;
using Ecs.Utils.Interfaces;
using JCMG.EntitasRedux.Commands;
using SimpleUi.Abstracts;
using UniRx;
using UnityEngine.EventSystems;

namespace Game.Ui.Input
{
    public class InputController : UiController<InputView>, IUiInitialize
    {
        private readonly ICommandBuffer _commandBuffer;
        
        public InputController(ICommandBuffer commandBuffer)
        {
            _commandBuffer = commandBuffer;
        }

        public void Initialize()
        {
            View.OnPointerDownCmd.Subscribe(OnPointerDown).AddTo(View);
            View.OnPointerUpCmd.Subscribe(OnPointerUp).AddTo(View);
            View.OnPointerDragCmd.Subscribe(OnDrag).AddTo(View);
        }
        
        private void OnPointerDown(PointerEventData eventData)
        {
            _commandBuffer.PointerDown(eventData.pointerId, eventData.position);
        }

        private void OnPointerUp(PointerEventData eventData)
        {
            _commandBuffer.PointerUp(eventData.pointerId);
        }
        
        private void OnDrag(PointerEventData eventData)
        {
            _commandBuffer.PointerDrag(eventData.pointerId, eventData.delta);
        }
    }
}