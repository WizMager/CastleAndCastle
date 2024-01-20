using Game.Ui.Building;
using Game.Ui.Input;
using SimpleUi;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameUiPrefabInstaller), fileName = nameof(GameUiPrefabInstaller))]
    public class GameUiPrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        
        [SerializeField] private BuildingPanelView buildingPanelView;
        [SerializeField] private InputView inputView;
        
        public override void InstallBindings()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(canvas);
            var canvasTransform = canvasView.transform;
            
            Container.BindUiView<BuildingPanelController, BuildingPanelView>(buildingPanelView, canvasTransform);
            Container.BindUiView<InputController, InputView>(inputView, canvasTransform);
        }
    }
}