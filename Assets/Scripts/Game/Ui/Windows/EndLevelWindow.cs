using Game.Ui.EndLevel;
using SimpleUi;

namespace Game.Ui.Windows
{
    public class EndLevelWindow : WindowBase
    {
        public override string Name => "EndLevelWindow";
        
        protected override void AddControllers()
        {
            AddController<EndLevelController>();
        }
    }
}