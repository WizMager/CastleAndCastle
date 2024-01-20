using UnityEngine;

namespace Game.Utils.Raycast
{
    public interface IRayCastProvider
    {
        bool Raycast(
            Vector3 origin,
            Vector3 direction,
            out RaycastHit hit,
            float distance,
            int layerMask,
            QueryTriggerInteraction triggerInteraction
        );

        bool SphereCast(
            Vector3 origin,
            float radius,
            Vector3 direction,
            out RaycastHit hit,
            float distance,
            int layerMask,
            QueryTriggerInteraction triggerInteraction
        );

        int OverlapSphereNonAlloc(
            Vector3 origin,
            float radius,
            Collider[] buffer,
            int layerMask,
            QueryTriggerInteraction triggerInteraction
        );

        int SphereCastNonAlloc(
            Vector3 origin,
            float radius,
            Vector3 direction,
            RaycastHit[] sphereCastBuffer,
            float distance,
            int defaultAndFloor,
            QueryTriggerInteraction ignore
        );

        int OverlapBoxNonAlloc(
            Vector3 center,
            Vector3 halfExtents,
            Collider[] results,
            Quaternion orientation,
            int mask,
            QueryTriggerInteraction queryTriggerInteraction
        );

        bool RayIntersectionWithBounds(Ray ray, Bounds bounds, out Vector3 pos);
    }
}