using System;

namespace Game.Utils.Units
{
    [Serializable]
    public readonly struct UnitData
    {
        public readonly float Health;
        public readonly float Damage;
        public readonly float AttackSpeed;
        public readonly float AttackRange;

        public UnitData(
            float health, 
            float damage, 
            float attackSpeed, 
            float attackRange
        )
        {
            Health = health;
            Damage = damage;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
        }
    }
}