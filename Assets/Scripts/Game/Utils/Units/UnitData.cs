using System;

namespace Game.Utils.Units
{
    [Serializable]
    public readonly struct UnitData
    {
        public readonly float MaxHealth;
        public readonly float Damage;
        public readonly float AttackSpeed;
        public readonly float AttackRange;

        public UnitData(
            float maxHealth, 
            float damage, 
            float attackSpeed, 
            float attackRange
        )
        {
            MaxHealth = maxHealth;
            Damage = damage;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
        }
    }
}