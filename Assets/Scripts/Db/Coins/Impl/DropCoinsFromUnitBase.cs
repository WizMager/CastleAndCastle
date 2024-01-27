using System;
using Game.Utils.Units;
using UnityEngine;

namespace Db.Coins.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(DropCoinsFromUnitBase), fileName = "DropCoinsFromUnitBase")]
    public class DropCoinsFromUnitBase : ScriptableObject, IDropCoinsFromUnitsBase
    {
        [SerializeField] private CoinsFromUnitVo[] coinsFromUnitsList;
        
        public int GetCoinsForUnitType(EUnitType unitType)
        {
            foreach (var element in coinsFromUnitsList)
            {
                if (element.unitType != unitType) continue;

                return element.dropCoins;
            }
            
            throw new Exception($"[{typeof(DropCoinsFromUnitBase)}]: Can't find unit type with name: {unitType}");
        }
    }
}