using System.Collections.Generic;

namespace Db.Buildings
{
    public interface IBuildingSettingsBase
    {
        IReadOnlyCollection<BuildingSettings> GetAll();
        BuildingSettings Get(EBuildingType buildingType);
    }
}