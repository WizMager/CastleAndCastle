using Game.Utils.Units;

namespace Db.Coins
{
    public interface IDropCoinsFromUnitsBase
    {
        int GetCoinsForUnitType(EUnitType unitType);
    }
}