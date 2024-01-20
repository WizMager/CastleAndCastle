using System;
using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Db.Buildings.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(BuildingSettingsBase), fileName = nameof(BuildingSettingsBase))]
    public class BuildingSettingsBase : ScriptableObject, 
        IBuildingSettingsBase
    {
        [KeyValue(nameof(BuildingSettings.Type))]
        [SerializeField] private BuildingSettings[] settingsArray;
        
        public IReadOnlyCollection<BuildingSettings> GetAll()
        {
            return settingsArray;
        }

        public BuildingSettings Get(EBuildingType buildingType)
        {
            foreach (var settings in settingsArray)
            {
                if (settings.Type == buildingType)
                    return settings;
            }

            throw new Exception(
                $"[{nameof(BuildingSettingsBase)}] can't find building settings with type {buildingType}");
        }
    }
}