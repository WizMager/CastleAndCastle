using SimpleUi.Abstracts;
using UniRx;
using UnityEngine.EventSystems;

namespace Game.Ui.Input
{
    public class InputView : UiView,
        IPointerDownHandler,
        IPointerUpHandler,
        IDragHandler
    {
        public ReactiveCommand<PointerEventData> OnPointerDownCmd { get; } = new();
        public ReactiveCommand<PointerEventData> OnPointerUpCmd { get; } = new();
        public ReactiveCommand<PointerEventData> OnPointerDragCmd { get; } = new();
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownCmd.Execute(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpCmd.Execute(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnPointerDragCmd.Execute(eventData);
        }
    }
}