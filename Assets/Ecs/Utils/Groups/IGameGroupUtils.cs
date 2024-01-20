using System;
using System.Collections.Generic;
using JCMG.EntitasRedux;

namespace Ecs.Utils.Groups
{
    public interface IGameGroupUtils
    {
        IDisposable GetObstacles(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
        IDisposable GetBuildingSlots(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
    }
}