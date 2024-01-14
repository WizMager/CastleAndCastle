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

        public GameGroupUtils(GameContext game)
        {
            _units = game.GetGroup(GameMatcher.AllOf(GameMatcher.UnitData, GameMatcher.UnitType));
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