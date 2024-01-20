using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Services.InputService.Impl
{
    public class UnityInputService : IInputService, ITickable
    {
        public event Action<int> MouseButtonDown;
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
                MouseButtonDown?.Invoke(0);
            
            if (Input.GetMouseButtonDown(1))
                MouseButtonDown?.Invoke(1);
        }
    }
}