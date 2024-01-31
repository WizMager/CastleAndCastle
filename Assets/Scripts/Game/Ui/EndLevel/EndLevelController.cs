using SimpleUi.Abstracts;

namespace Game.Ui.EndLevel
{
    public class EndLevelController : UiController<EndLevelView>
    {
        private readonly GameContext _game;

        public EndLevelController(GameContext game)
        {
            _game = game;
        }
        
        public override void OnShow()
        {
            if (_game.PlayerCastleEntity.IsDead)
            {
                View.endLevelStateText.text = "You Lose... =(";
            }
            else
            {
                View.endLevelStateText.text = "You Win!!! =)";
            }
        }
    }
}