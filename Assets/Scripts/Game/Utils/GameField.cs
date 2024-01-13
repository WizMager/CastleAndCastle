using UnityEngine;

namespace Game.Utils
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Vector3 playerCastlePosition;
        [SerializeField] private Vector3 enemyCastlePosition;

        public Vector3 PlayerCastlePosition => playerCastlePosition;
        public Vector3 EnemyCastlePosition => enemyCastlePosition;
    }
}