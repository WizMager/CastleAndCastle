using Ecs.Views.Linkable.Impl.Building;
using UnityEngine;

namespace Game.Utils
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Transform playerCastlePosition;
        [SerializeField] private Transform enemyCastlePosition;
        [SerializeField] private Transform startCameraPosition;
        [SerializeField] private BuildingSlotView[] buildingSlotViews;

        public Vector3 PlayerCastlePosition => playerCastlePosition.position;
        public Vector3 EnemyCastlePosition => enemyCastlePosition.position;
        public BuildingSlotView[] BuildingSlotViews => buildingSlotViews;
        public Transform StartCameraPosition => startCameraPosition;
    }
}