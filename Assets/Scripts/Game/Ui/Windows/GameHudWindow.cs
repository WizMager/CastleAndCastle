using Game.Ui.Building;
using Game.Ui.Income;
using Game.Ui.Input;
using SimpleUi;

namespace Game.Ui.Windows
{
    public class GameHudWindow : WindowBase
    {
        public override string Name => "GameHudWindow";

        protected override void AddControllers()
        {
            AddController<BuildingPanelController>();
            AddController<InputController>();
            AddController<CoinsController>();
        }
    }
}