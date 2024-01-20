using Db.Buildings;
using Db.Buildings.Impl;
using Db.Camera;
using Db.Camera.Impl;
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
        [SerializeField] private BuildingSettingsBase buildingSettingsBase;
        [SerializeField] private CameraBase cameraBase;

        public override void InstallBindings()
        {
            Container.Bind<IPrefabsBase>().FromInstance(prefabsBase);
            Container.Bind<IBuildingSettingsBase>().FromInstance(buildingSettingsBase);
            Container.Bind<ICameraBase>().FromInstance(cameraBase);
        }
    }
}