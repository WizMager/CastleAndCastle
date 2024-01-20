using System;
using System.Collections.Generic;
using Game.Utils;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using UnityEngine.Pool;

namespace Ecs.Utils.Groups.Impl
{
    public class GameGroupUtils : IGameGroupUtils
    {
        private readonly IGroup<GameEntity> _units;
        private readonly IGroup<GameEntity> _buildingSlotsGroup;

        public GameGroupUtils(GameContext game)
        {
            _units = game.GetGroup(GameMatcher.AllOf(GameMatcher.UnitData, GameMatcher.UnitType));
            _buildingSlotsGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.BuildingSlot));
        }

        public IDisposable GetUnits(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null, bool nonDestroyed = true)
        {
            Func<GameEntity, bool> baseFilter = nonDestroyed
                ? e => !e.IsDestroyed
                : e => e.IsDestroyed;
            
            return GetEntities(out buffer, _units, baseFilter, filter);
        }

        public IDisposable GetOwnerUnits(out List<GameEntity> buffer, bool isPlayerUnits, Func<GameEntity, bool> filter = null)
        {
            Func<GameEntity, bool> baseFilter = isPlayerUnits
                ? e => e.IsPlayerUnit && !e.IsDestroyed
                : e => !e.IsPlayerUnit && !e.IsDestroyed;
            
            return GetEntities(out buffer, _units, baseFilter, filter);
        }

        public IDisposable GetOwnerUnitsWithType(out List<GameEntity> buffer, bool isPlayerUnits, EUnitType unitType, Func<GameEntity, bool> filter = null)
        {
            Func<GameEntity, bool> baseFilter = isPlayerUnits
                ? e => e.IsPlayerUnit && e.UnitType.Value == unitType && !e.IsDestroyed
                : e => !e.IsPlayerUnit && e.UnitType.Value == unitType && !e.IsDestroyed;
            
            return GetEntities(out buffer, _units, baseFilter, filter);
        }

        public IDisposable GetBuildingSlots(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            return GetEntities(out buffer, _buildingSlotsGroup, e => e.IsBuildingSlot && !e.IsDestroyed, filter);
        }
        

        private IDisposable GetEntities(
            out List<GameEntity> buffer,  
            IGroup<GameEntity> group,
            Func<GameEntity, bool> baseFilter, 
            Func<GameEntity, bool> filter = null)
        {
            var pooledObject = ListPool<GameEntity>.Get(out buffer);
            group.GetEntities(buffer);
            
            if (filter != null)
            {
                buffer.RemoveAllWithSwap(e => !(baseFilter(e) && filter(e)));    
            }
            else
            {
                buffer.RemoveAllWithSwap(e => !baseFilter(e));
            }
            
            return pooledObject;
        }
    }
}