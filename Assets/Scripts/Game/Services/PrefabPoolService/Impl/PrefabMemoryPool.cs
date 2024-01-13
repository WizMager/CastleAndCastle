using Ecs.Views.Linkable;
using UnityEngine;
using Zenject;

namespace Game.Services.PrefabPoolService.Impl
{
    public class PrefabMemoryPool : MemoryPool<IObjectLinkable>, IPrefabMemoryPool
    {
        private static readonly Vector3 DefaultPosition = new (0, -5000, 0);

        private Transform _originalParent;

        public string Name { get; }

        public PrefabMemoryPool(string name)
        {
            Name = name;
        }

        protected override void OnCreated(IObjectLinkable item)
        {
            var itemTransform = item.Transform;
            itemTransform.SetPositionAndRotation(DefaultPosition, Quaternion.identity);

#if UNITY_EDITOR
            // Record the original parent which will be set to whatever is used in the UnderTransform method
            if (_originalParent == null)
                _originalParent = itemTransform.parent;
#endif
        }

        protected override void OnDestroyed(IObjectLinkable item)
        {
            Object.Destroy(item.Transform.gameObject);
        }

        protected override void OnSpawned(IObjectLinkable item)
        {
            //item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(IObjectLinkable item)
        {
            var itemTransform = item.Transform;
            itemTransform.SetPositionAndRotation(DefaultPosition, Quaternion.identity);

#if UNITY_EDITOR
            var parent = itemTransform.transform.parent;
            if (_originalParent == null && parent == null)
                return;
            if (_originalParent == null && parent != null
                || parent.GetInstanceID() != _originalParent.GetInstanceID())
            {
                itemTransform.transform.SetParent(_originalParent, false);
            }
#endif
        }
    }
}