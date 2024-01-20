namespace Game.Services.GameLevelViewProvider.Impl
{
    public class GameLevelViewProvider : IGameLevelViewProvider
    {
        public GameLevelViewProvider(GameLevelView levelView)
        {
            LevelView = levelView;
        }

        public GameLevelView LevelView { get; }
    }
}