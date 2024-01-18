﻿using Ecs.Extensions.UidGenerator;
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

            if (isPlayerUnit)
            {
                entity.IsPlayerUnit = true;
            }
            
            return entity;
        }
        
        public static GameEntity CreateCamera(
            this GameContext game, 
            Transform spawnPosition
        )
        {
            var entity = game.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            entity.AddPrefab("GameCamera");
            entity.AddPosition(spawnPosition.position);
            entity.AddRotation(spawnPosition.rotation);
            entity.IsCamera = true;
            entity.IsInstantiate = true;
            
            return entity;
        }
    }
}