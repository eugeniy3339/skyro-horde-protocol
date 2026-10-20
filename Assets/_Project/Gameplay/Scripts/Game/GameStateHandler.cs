using System;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public static class GameStateHandler
    {
        private static GameState _cgs = GameState.Run;
        public static GameState CurState
        {
            get { return _cgs; }
            private set
            {
                if (_cgs == value) return;
                _cgs = value;
                onGameStateChanged?.Invoke(value);
            }
        }

        public static Action<GameState> onGameStateChanged;



        public static void SetGameState(GameState state)
        {
            CurState = state;
        }



        public enum GameState
        {
            Run,
            Pause,
            GameOver
        }
    }
}
