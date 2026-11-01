using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HordeProtocol.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event Action onGameOver;

        private void Awake()
        {
            Instance = this;
            GameStateHandler.SetGameState(GameStateHandler.GameState.Running);
        }

        private void OnPlayerDied()
        {
            onGameOver?.Invoke();
            GameStateHandler.SetGameState(GameStateHandler.GameState.GameOver);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



        private void OnEnable()
        {
            PlayerHealth.onPlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            PlayerHealth.onPlayerDied -= OnPlayerDied;
        }
    }
}
