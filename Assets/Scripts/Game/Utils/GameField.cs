using UnityEngine;

namespace Game.Utils
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Transform playerCastlePosition;
        [SerializeField] private Transform enemyCastlePosition;

        public Vector3 PlayerCastlePosition => playerCastlePosition.position;
        public Vector3 EnemyCastlePosition => enemyCastlePosition.position;
    }
}