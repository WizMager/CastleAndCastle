using SimpleUi.Abstracts;

namespace Game.Ui.StartGame
{
    public class StartGameController : UiController<StartGameView>
    {
        public override void OnShow()
        {
            View.button.gameObject.SetActive(true);
        }
    }
}