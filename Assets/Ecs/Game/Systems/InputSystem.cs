using System;
using Game.Services.InputService;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems
{
    public class InputSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly IInputService _inputService;

        public InputSystem(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public void Initialize()
        {
            _inputService.MouseButtonDown += OnMouseDown;
        }
        
        public void Dispose()
        {
            _inputService.MouseButtonDown -= OnMouseDown;
        }

        private void OnMouseDown(int mouseButton)
        {
            
        }
    }
}