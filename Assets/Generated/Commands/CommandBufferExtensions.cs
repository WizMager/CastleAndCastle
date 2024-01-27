//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Commands Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using JCMG.EntitasRedux.Commands;
using Ecs.Commands.Command;
using Ecs.Extensions.UidGenerator;
using System;
using UnityEngine;
using Game.Utils.Units;
using Ecs.Commands.Command.Input;
using Ecs.Commands.Command.Income;
using Ecs.Commands.Command.Buildings;
using Db.Buildings;

namespace Generated.Commands
{
    public static partial class CommandBufferExtensions
    {
        public static void ReceiveDamage(this ICommandBuffer commandBuffer, Uid targetUid, Single damage)
        {
            ref var command = ref commandBuffer.Create<ReceiveDamageCommand>();
            command.TargetUid = targetUid;
            command.Damage = damage;
        }

        public static void SpawnUnit(this ICommandBuffer commandBuffer, Vector3 position, Quaternion rotation, EUnitType unitType, Boolean isPlayerUnit)
        {
            ref var command = ref commandBuffer.Create<SpawnUnitCommand>();
            command.Position = position;
            command.Rotation = rotation;
            command.UnitType = unitType;
            command.IsPlayerUnit = isPlayerUnit;
        }

        public static void PointerDown(this ICommandBuffer commandBuffer, Int32 touchId)
        {
            ref var command = ref commandBuffer.Create<PointerDownCommand>();
            command.TouchId = touchId;
        }

        public static void PointerDrag(this ICommandBuffer commandBuffer, Int32 touchId, Vector3 delta)
        {
            ref var command = ref commandBuffer.Create<PointerDragCommand>();
            command.TouchId = touchId;
            command.Delta = delta;
        }

        public static void PointerUp(this ICommandBuffer commandBuffer, Int32 touchId)
        {
            ref var command = ref commandBuffer.Create<PointerUpCommand>();
            command.TouchId = touchId;
        }

        public static void AddCoins(this ICommandBuffer commandBuffer, Int32 value)
        {
            ref var command = ref commandBuffer.Create<AddCoinsCommand>();
            command.Value = value;
        }

        public static void EnterBuildingMode(this ICommandBuffer commandBuffer, EBuildingType buildingType)
        {
            ref var command = ref commandBuffer.Create<EnterBuildingModeCommand>();
            command.BuildingType = buildingType;
        }

        public static void ExitBuildingMode(this ICommandBuffer commandBuffer)
        {
            ref var command = ref commandBuffer.Create<ExitBuildingModeCommand>();
        }

        public static void MouseDown(this ICommandBuffer commandBuffer, Int32 button)
        {
            ref var command = ref commandBuffer.Create<MouseDownCommand>();
            command.Button = button;
        }
    }
}

