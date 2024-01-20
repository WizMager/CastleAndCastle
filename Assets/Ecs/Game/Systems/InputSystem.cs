﻿using System;
using Ecs.Commands.Commands;
using Game.Services.InputService;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.Game.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 20, nameof(EFeatures.Input))]
    public class InputSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly IInputService _inputService;
        private readonly ICommandBuffer _commandBuffer;

        public InputSystem(IInputService inputService, ICommandBuffer commandBuffer)
        {
            _inputService = inputService;
            _commandBuffer = commandBuffer;
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
            var cmd = new MouseDownCommand();
            cmd.Button = mouseButton;
            
            _commandBuffer.Create(cmd);
        }
        
    }
}