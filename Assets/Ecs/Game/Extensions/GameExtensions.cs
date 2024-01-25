﻿using Db.Buildings;
using Ecs.Extensions.UidGenerator;
using Game.Utils.Units;
using UnityEngine;

namespace Ecs.Game.Extensions
{
    public static class GameExtensions
    {
        public static GameEntity CreateUnit(
            this GameContext game, 
            Vector3 position, 
            Quaternion rotation, 
            EUnitType unitType,
            bool isPlayerUnit
        )
        {
            var entity = game.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            entity.AddPrefab(unitType.ToString());
            entity.AddUnitType(unitType);
            entity.AddPosition(position);
            entity.AddRotation(rotation);
            entity.IsInstantiate = true;
            entity.ReplaceUnitState(EUnitState.Walk);

            if (isPlayerUnit)
            {
                entity.IsPlayer = true;
            }
            
            return entity;
        }
        
        public static GameEntity CreateCamera(
            this GameContext game, 
            Transform cameraTransform
        )
        {
            var entity = game.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            entity.AddPosition(cameraTransform.position);
            entity.AddRotation(cameraTransform.rotation);
            entity.IsCamera = true;
            
            return entity;
        }
        
        public static GameEntity CreateBuilding(
            this GameContext game, 
            Vector3 buildingSlotPosition,
            Quaternion buildingSlotRotation,
            EBuildingType buildingType,
            bool isPlayerBuilding
        )
        {
            var building = game.CreateEntity();
            building.AddPrefab(buildingType.ToString());
            building.AddPosition(buildingSlotPosition);
            building.AddRotation(buildingSlotRotation);
            building.AddBuildingType(buildingType);
            building.IsInstantiate = true;

            if (isPlayerBuilding)
            {
                building.IsPlayer = true;
            }
            
            return building;
        }
    }
}