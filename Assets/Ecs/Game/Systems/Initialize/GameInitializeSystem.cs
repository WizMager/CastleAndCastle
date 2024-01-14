using Ecs.Game.Extensions;
using Game.Providers.GameFieldProvider;
using Game.Ui;
using Game.Utils.Units;
using JCMG.EntitasRedux;
using SimpleUi.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    public class GameInitializeSystem : IInitializeSystem
    {
        private readonly SignalBus _signalBus;
        private readonly GameContext _game;
        private readonly IGameFieldProvider _gameFieldProvider;

        public GameInitializeSystem(
            SignalBus signalBus,
            GameContext game,
            IGameFieldProvider gameFieldProvider
        )
        {
            _signalBus = signalBus;
            _game = game;
            _gameFieldProvider = gameFieldProvider;
        }
        
        public void Initialize()
        {
            Observable.TimerFrame(100).Subscribe(_ =>
            {
                _signalBus.OpenWindow<GameWindow>();
            });
            
            DebugSpawnUnits();
        }

        private void DebugSpawnUnits()
        {
            var playerCastle = _gameFieldProvider.GameField.PlayerCastlePosition;
            var enemyCastle = _gameFieldProvider.GameField.EnemyCastlePosition;
            
            for (int i = 0; i < 3; i++)
            {
                var unit = _game.CreateUnit(new Vector3(-40 + i, 0.5f, 0), Quaternion.identity, EUnitType.MeleeUnit, false);
                unit.ReplaceDestinationPoint(playerCastle);
            }
            
            for (int i = 0; i < 2; i++)
            {
                var unit = _game.CreateUnit(new Vector3(-40 + i, 0.5f, 1 + i), Quaternion.identity, EUnitType.RangeUnit, false);
                unit.ReplaceDestinationPoint(playerCastle);
            }
            
            for (int i = 0; i < 3; i++)
            {
                var unit = _game.CreateUnit(new Vector3(40 - i, 0.5f, 0), Quaternion.identity, EUnitType.MeleeUnit, true);
                unit.ReplaceDestinationPoint(enemyCastle);
            }
            
            for (int i = 0; i < 2; i++)
            {
                var unit = _game.CreateUnit(new Vector3(40 - i, 0.5f, 1 + i), Quaternion.identity, EUnitType.RangeUnit, true);
                unit.ReplaceDestinationPoint(enemyCastle);
            }
        }
    }
}