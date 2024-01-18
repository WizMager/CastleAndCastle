using UnityEngine;

namespace Game.Utils
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Transform playerCastlePosition;
        [SerializeField] private Transform enemyCastlePosition;
        [SerializeField] private Transform startCameraPosition;

        public Vector3 PlayerCastlePosition => playerCastlePosition.position;
        public Vector3 EnemyCastlePosition => enemyCastlePosition.position;
        public Transform StartCameraPosition => startCameraPosition;
    }
}