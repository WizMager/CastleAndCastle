using System;
using UnityEngine;

namespace Db.Prefabs.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PrefabsBase), fileName = "PrefabsBase")]
    public class PrefabsBase : ScriptableObject, IPrefabsBase
    {
        [SerializeField] private Prefab[] prefabs;

        public GameObject Get(string prefabName)
        {
            for (var i = 0; i < prefabs.Length; i++)
            {
                var prefab = prefabs[i];
                if (prefab.name == prefabName)
                    return prefab.gameObject;
            }

            throw new Exception($"[{typeof(PrefabsBase)}]: Can't find prefab with name: {prefabName}");
        }

        [Serializable]
        public class Prefab
        {
            public string name;
            public GameObject gameObject;
        }
    }
}