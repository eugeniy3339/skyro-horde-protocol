using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            GameStateHandler.SetGameState(GameStateHandler.GameState.Run);
        }

        private void OnPlayerDied()
        {
            GameStateHandler.SetGameState(GameStateHandler.GameState.GameOver);
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}
