﻿using Ecs.Utils.Interfaces;
using SimpleUi.Abstracts;
using UniRx;

namespace Game.Ui.Income
{
    public class CoinsController : UiController<CoinsView>, IUiInitialize
    {
        private readonly GameContext _game;

        public CoinsController(GameContext game)
        {
            _game = game;
        }

        public void Initialize()
        {
            _game.CoinsEntity.SubscribeCoins(OnCoinsChanged).AddTo(View.gameObject);
        }

        private void OnCoinsChanged(GameEntity _, int playerCoins, int enemyCoins)
        {
            View.CoinsText.text = $"{playerCoins}";
        }
    }
}