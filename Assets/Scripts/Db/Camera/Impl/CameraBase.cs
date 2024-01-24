using UnityEngine;

namespace Db.Camera.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(CameraBase), fileName = "CameraBase")]
    public class CameraBase : ScriptableObject, ICameraBase
    {
        [SerializeField] private float moveThreshold = 0.1f;
        [SerializeField] private float moveSensitive = 10f;

        public float MoveThreshold => moveThreshold;
        public float MoveSensitive => moveSensitive;
    }
}