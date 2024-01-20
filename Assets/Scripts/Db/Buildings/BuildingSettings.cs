using System;
using UnityEngine;

namespace Db.Buildings
{
    [Serializable]
    public class BuildingSettings
    {
        public EBuildingType Type;
        public string Name;
        public Sprite Icon;
        public int Price;
        public int Income;
    }
}