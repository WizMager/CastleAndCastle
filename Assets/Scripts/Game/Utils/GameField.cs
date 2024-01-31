using Ecs.Views.Linkable.Impl.Building;
using UnityEngine;

namespace Game.Utils
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Transform startCameraPosition;
        [SerializeField] private BuildingSlotView[] buildingSlotViews;
        [SerializeField] private CastleView playerCastle;
        [SerializeField] private CastleView enemyCastle;
        
        public BuildingSlotView[] BuildingSlotViews => buildingSlotViews;
        public Transform StartCameraPosition => startCameraPosition;
        public CastleView PlayerCastle => playerCastle;
        public CastleView EnemyCastle => enemyCastle;
    }
}