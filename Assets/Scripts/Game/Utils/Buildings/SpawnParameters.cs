using UnityEngine;

namespace Game.Utils.Buildings
{
    public readonly struct SpawnParameters
    {
        public readonly float SpawnCooldown;
        public readonly Vector3 SpawnPosition;
        public readonly Quaternion SpawnRotation;

        public SpawnParameters(
            float spawnCooldown, 
            Vector3 spawnPosition, 
            Quaternion spawnRotation
        )
        {
            SpawnCooldown = spawnCooldown;
            SpawnPosition = spawnPosition;
            SpawnRotation = spawnRotation;
        }
    }
}