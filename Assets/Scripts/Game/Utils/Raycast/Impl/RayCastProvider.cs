using Game.Providers.CameraProvider;
using UnityEngine;

namespace Game.Utils.Raycast.Impl
{
    public class RayCastProvider : IRayCastProvider
	{
		private const float START_RAY_CAST_Y = 10f;
		private const float RAY_CAST_DISTANCE = 50f;
		private const float PLAYER_UP_OFFSET = 0.5f;
		private const string SAND_TAG = "Sand";

		private readonly GameContext _game;
		private readonly ICameraProvider _cameraProvider;

		private Vector3 _position = new Vector3(0, START_RAY_CAST_Y, 0);
		private Ray _ray = new Ray(Vector3.zero, Vector3.down);

		private readonly int _groundLayer;
		private RaycastHit _hit;

		public RayCastProvider(GameContext game, ICameraProvider cameraProvider)
		{
			_game = game;
			_cameraProvider = cameraProvider;
			_groundLayer = LayerMask.NameToLayer("Ground");
		}

		public Vector3 GetMapPositionByRayCast(float x, float z)
		{
			_position.x = x;
			_position.z = z;

			var y = Physics.Raycast(_position, Vector3.down, out var hit, RAY_CAST_DISTANCE, _groundLayer,
				QueryTriggerInteraction.Ignore)
				? hit.point.y
				: 0f;

			return new Vector3(x, y, z);
		}

		public Vector3 GetMapPositionByLayerAndTag(float x, float z, out bool isGroundLayer)
		{
			_position.x = x;
			_position.z = z;

			isGroundLayer = false;
			if (Physics.BoxCast(_position, Vector3.one * 0.5f, Vector3.down, out _hit, Quaternion.identity,
				    RAY_CAST_DISTANCE))
			{
				if (_hit.transform.gameObject.layer == _groundLayer && _hit.transform.gameObject.CompareTag(SAND_TAG))
					isGroundLayer = true;
			}

			var y = isGroundLayer ? _hit.point.y : 0f;

			return new Vector3(x, y, z);
		}
		

		// public Vector3 GetHitPoint(int layer, out Vector3 hitNormal, out float distance, out int objectHash)
		// {
		// 	objectHash = -1;
		// 	hitNormal = Vector3.zero;
		// 	distance = 50f;
		// 	var camera = _cameraProvider.GetCamera();
		// 	if (camera == null)
		// 		return Vector3.zero;
		//
		// 	var transform = camera.transform;
		// 	_ray = new Ray(transform.position, transform.forward);
		// 	if (!Physics.Raycast(_ray.origin, _ray.direction, out _hit,
		// 		    distance, layer, QueryTriggerInteraction.Ignore))
		// 		return Vector3.zero;
		//
		// 	hitNormal = _hit.normal;
		// 	distance = _hit.distance;
		// 	var component = _hit.collider.transform.GetComponent<IObjectHash>();
		// 	objectHash = component?.Hash ?? -1;
		// 	return _hit.point;
		// }

		// public bool GetHitObject(
		// 	Vector3 direction,
		// 	int layer,
		// 	float distance,
		// 	out RaycastHit hit,
		// 	out int objectHash
		// )
		// {
		// 	hit = new RaycastHit();
		// 	objectHash = -1;
		// 	var player = _game.PlayerEntity;
		// 	if (player == null)
		// 		return false;
		//
		// 	var origin = player.LookPoint.Value.position;
		// 	_ray = new Ray(origin, direction);
		// 	if (!Physics.Raycast(_ray.origin, _ray.direction, out hit,
		// 		    distance, layer, QueryTriggerInteraction.Ignore))
		// 		return false;
		//
		// 	//var component = hit.collider.transform.GetComponent<IObjectHash>();
		// 	//objectHash = component?.Hash ?? -1;
		// 	
		// 	hit.collider.transform
		// 	return true;
		// }
		
		p

		public bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hit,
			float distance,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		)
			=> Physics.Raycast(origin, direction, out hit, distance, layerMask, triggerInteraction);

		public bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float distance,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		)
			=> Physics.SphereCast(origin, radius, direction, out hit, distance, layerMask, triggerInteraction);

		public int OverlapSphereNonAlloc(
			Vector3 origin,
			float radius,
			Collider[] buffer,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		)
			=> Physics.OverlapSphereNonAlloc(origin, radius, buffer, layerMask, triggerInteraction);

		public int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] buffer,
			float distance,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		)
			=> Physics.SphereCastNonAlloc(origin, radius, direction, buffer, distance, layerMask, triggerInteraction);

		public int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation,
			int mask,
			QueryTriggerInteraction queryTriggerInteraction
		)
			=> Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, queryTriggerInteraction);

		public bool RayIntersectionWithBounds(Ray ray, Bounds bounds, out Vector3 pos)
		{
			pos = default;
            
			var dx = 1.0f / ray.direction.x;
			var dy = 1.0f / ray.direction.y;
			var dz = 1.0f / ray.direction.z;
            
			// lb is the corner of AABB with minimal coordinates - left bottom, rt is maximal corner
			// r.org is origin of ray
			var t1 = (bounds.min.x - ray.origin.x) * dx;
			var t2 = (bounds.max.x - ray.origin.x) * dx;
			var t3 = (bounds.min.y - ray.origin.y) * dy;
			var t4 = (bounds.max.y - ray.origin.y) * dy;
			var t5 = (bounds.min.z - ray.origin.z) * dz;
			var t6 = (bounds.max.z - ray.origin.z) * dz;

			var tMin = Mathf.Max(Mathf.Max(Mathf.Min(t1, t2), Mathf.Min(t3, t4)), Mathf.Min(t5, t6));
			var tMax = Mathf.Min(Mathf.Min(Mathf.Max(t1, t2), Mathf.Max(t3, t4)), Mathf.Max(t5, t6));

			// if tmax < 0, ray (line) is intersecting AABB, but the whole AABB is behind us
			if (tMax < 0)
				return false;

			// if tmin > tmax, ray doesn't intersect AABB
			if (tMin > tMax)
				return false;

			pos =  ray.origin + ray.direction * tMin;
            
			return true;
		}
	}
}