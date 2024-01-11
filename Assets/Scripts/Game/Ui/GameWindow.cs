using Game.Ui.StartGame;
using SimpleUi;

namespace Game.Ui
{
    public class GameWindow : WindowBase
    {
        public override string Name => "Game";
        
        protected override void AddControllers()
        {
            AddController<StartGameController>();
        }
    }
}