using System;
using System.Collections.Generic;
using JCMG.EntitasRedux;
using Game.Utils.Units;

namespace Ecs.Utils.Groups
{
    public interface IGameGroupUtils
    {
        IDisposable GetUnits(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null, bool nonDestroyed = true);
        IDisposable GetOwnerUnits(out List<GameEntity> buffer, bool isPlayerUnits, Func<GameEntity, bool> filter = null);
        IDisposable GetOwnerUnitsWithType(out List<GameEntity> buffer, bool isPlayerUnits, EUnitType unitType, Func<GameEntity, bool> filter = null);
        IDisposable GetBuildingSlots(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
    }
}