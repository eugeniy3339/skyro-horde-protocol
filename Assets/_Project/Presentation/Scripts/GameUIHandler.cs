using HordeProtocol.Gameplay;
using TMPro;
using UnityEngine;

namespace HordeProtocol.Presentation
{
    public class GameUIHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text curStateText;

        private void Awake()
        {
            SetCurStateText(GameStateHandler.CurState);
        }

        private void OnCurStateChanged(GameStateHandler.GameState state)
        {
            SetCurStateText(state);
        }

        private void SetCurStateText(GameStateHandler.GameState state)
        {
            curStateText.text = state.ToString();
        }

        private void OnEnable()
        {
            GameStateHandler.onGameStateChanged += OnCurStateChanged;
        }

        private void OnDisable()
        {
            GameStateHandler.onGameStateChanged -= OnCurStateChanged;
        }
    }
}
