using System;
using System.Collections.Generic;
using Game.Utils.Units;

namespace Ecs.Utils.Groups
{
    public interface IGameGroupUtils
    {
        IDisposable GetUnits(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null, bool nonDestroyed = true);

        public IDisposable GetUnitsWithType(out List<GameEntity> buffer, EUnitType unitType, Func<GameEntity, bool> filter = null);

        IDisposable GetOwnerUnits(out List<GameEntity> buffer, bool isPlayerUnits, Func<GameEntity, bool> filter = null);
    }
}