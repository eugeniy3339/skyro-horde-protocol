using System;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public static class PauseManager
    {
        private static bool isPaused;
        public static bool IsPaused { get { return isPaused; } }

        public static event Action<bool> onPauseChanged;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Awake()
        {
            GameManager.onGameOver += OnGameOver;
        }

        public static void PauseUnpause()
        {
            PauseUnpause(!isPaused);
        }

        public static void PauseUnpause(bool toPause)
        {
            if (toPause == isPaused) return;
            if (toPause && GameStateHandler.CurState == GameStateHandler.GameState.GameOver) return;

            isPaused = toPause;
            Time.timeScale = toPause ? 0f : 1f;
            GameStateHandler.SetGameState(toPause ? GameStateHandler.GameState.Pause : GameStateHandler.GameState.Running);
            onPauseChanged?.Invoke(toPause);
        }

        

        private static void OnGameOver()
        {
            PauseUnpause(false);
        }
    }
}
