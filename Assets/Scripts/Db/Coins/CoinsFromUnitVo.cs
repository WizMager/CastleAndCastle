using System;
using Game.Utils.Units;

namespace Db.Coins
{
    [Serializable]
    public class CoinsFromUnitVo
    {
        public EUnitType unitType;
        public int dropCoins;
    }
}