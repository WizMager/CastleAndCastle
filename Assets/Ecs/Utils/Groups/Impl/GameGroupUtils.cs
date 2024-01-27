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
        private readonly IGroup<GameEntity> _unitsGroup;
        private readonly IGroup<GameEntity> _buildingSlotsGroup;
        private readonly IGroup<GameEntity> _buildingsGroup;

        public GameGroupUtils(GameContext game)
        {
            _unitsGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.UnitData, GameMatcher.UnitType));
            _buildingSlotsGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.BuildingSlot));
            _buildingsGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.BuildingType));
        }

        public IDisposable GetUnits(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null, bool nonDestroyed = true)
        {
            Func<GameEntity, bool> baseFilter = nonDestroyed
                ? e => !e.IsDestroyed
                : e => e.IsDestroyed;
            
            return GetEntities(out buffer, _unitsGroup, baseFilter, filter);
        }

        public IDisposable GetOwnerUnits(out List<GameEntity> buffer, bool isPlayerUnits, Func<GameEntity, bool> filter = null)
        {
            Func<GameEntity, bool> baseFilter = isPlayerUnits
                ? e => e.IsPlayer && !e.IsDestroyed
                : e => !e.IsPlayer && !e.IsDestroyed;
            
            return GetEntities(out buffer, _unitsGroup, baseFilter, filter);
        }

        public IDisposable GetOwnerUnitsWithType(out List<GameEntity> buffer, bool isPlayerUnits, EUnitType unitType, Func<GameEntity, bool> filter = null)
        {
            Func<GameEntity, bool> baseFilter = isPlayerUnits
                ? e => e.IsPlayer && e.UnitType.Value == unitType && !e.IsDestroyed
                : e => !e.IsPlayer && e.UnitType.Value == unitType && !e.IsDestroyed;
            
            return GetEntities(out buffer, _unitsGroup, baseFilter, filter);
        }

        public IDisposable GetBuildingSlots(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            return GetEntities(out buffer, _buildingSlotsGroup, e => e.IsBuildingSlot && !e.IsDestroyed, filter);
        }
        
        public IDisposable GetSpawnableBuilding(out List<GameEntity> buffer, bool withCooldown, Func<GameEntity, bool> filter = null)
        {
            Func<GameEntity, bool> baseFilter = withCooldown
                ? e => e.HasSpawnParameters && e.HasTime && !e.IsDestroyed
                : e => e.HasSpawnParameters && !e.HasTime && !e.IsDestroyed;
            
            return GetEntities(out buffer, _buildingsGroup, baseFilter, filter);
        }

        public IDisposable GetBuildingsIncome(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            return GetEntities(out buffer, _buildingsGroup, e => e.IsBuilding && e.HasIncome);
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