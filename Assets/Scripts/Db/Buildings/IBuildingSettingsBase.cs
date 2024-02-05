using System.Collections.Generic;

namespace Db.Buildings
{
    public interface IBuildingSettingsBase
    {
        float EnemyBuildCooldown { get; }
        
        IReadOnlyCollection<BuildingSettings> GetAll();
        BuildingSettings Get(EBuildingType buildingType);
    }
}