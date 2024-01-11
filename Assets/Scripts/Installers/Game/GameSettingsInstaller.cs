using Db.Prefabs;
using Db.Prefabs.Impl;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameSettingsInstaller), fileName = nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PrefabsBase prefabsBase;

        public override void InstallBindings()
        {
            Container.Bind<IPrefabsBase>().FromInstance(prefabsBase);
        }
    }
}