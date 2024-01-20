using System;
using UnityEngine;

namespace Game.Services.InputService
{
    public interface IInputService
    {
        event Action<int> MouseButtonDown;
        Vector3 MousePosition { get; }
    }
}