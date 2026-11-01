using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace HordeProtocol.Gameplay
{

    public class PlayerInputsManager : MonoBehaviour
    {
        private Movement movement;
        private Dash dash;
        private Shooting shooting;

        private Horde playerInputs;

        private Vector2 lastMoveInput;
        private bool isShooting = false;

        private void Awake()
        {
            movement = GetComponent<Movement>();
            dash = GetComponent<Dash>();
            shooting = GetComponent<Shooting>();

            playerInputs = new Horde();

            playerInputs.Player.Move.started += OnMove;
            playerInputs.Player.Move.performed += OnMove;
            playerInputs.Player.Move.canceled += OnMove;

            playerInputs.Player.Dash.started += OnDash;

            playerInputs.Player.Fire.started += OnFire; 
            playerInputs.Player.Fire.canceled += OnFire;

            playerInputs.GameState.Pause.started += OnPause;
            playerInputs.GameState.Retry.started += OnRestart;
            playerInputs.GameState.Enable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (GameStateHandler.CurState == GameStateHandler.GameState.GameOver) return;

            Vector2 input = context.ReadValue<Vector2>();
            SetMoveInputs(input);
        }

        private void SetMoveInputs(Vector2 input)
        {
            movement.moveDir = input;

            if (input.magnitude > 0.1f)
                lastMoveInput = input;
        }

        private void OnDash(InputAction.CallbackContext context)
        {
            if (GameStateHandler.CurState != GameStateHandler.GameState.Running) return;
            if (!context.started) return;
            dash.DashIfCanTo(lastMoveInput);
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            if(GameStateHandler.CurState == GameStateHandler.GameState.GameOver) return;
            if (context.performed) return;

            SetFireInput(context.started);
        }

        private void SetFireInput(bool fire)
        {
            isShooting = fire;
            if (isShooting)
            {
                shooting.onShootCooldown += OnShootCooldown;
                Fire();
            }
            else
            {
                shooting.onShootCooldown -= OnShootCooldown;
            }
        }

        private void OnShootCooldown()
        {
            if (!isShooting) return;
            Fire();
        } 

        private void Fire()
        {
            if (GameStateHandler.CurState != GameStateHandler.GameState.Running) return;
            Vector2 mousePos = playerInputs.Player.Mouse.ReadValue<Vector2>();
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 dirToTheMousePos = worldMousePos - (Vector2)transform.position;

            shooting.FireIfCanTo(dirToTheMousePos);
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            if (GameStateHandler.CurState == GameStateHandler.GameState.GameOver) return;
            if (!context.started) return;

            PauseManager.PauseUnpause();
        }

        private void OnPauseChanged(bool pause)
        {
            if (pause) return;

            SetFireInput(isShooting);
        }

        private void OnRestart(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            GameManager.Instance?.RestartGame();
        }



        private void OnEnable()
        {
            playerInputs.Player.Enable();
            PauseManager.onPauseChanged += OnPauseChanged;
        }

        private void OnDisable()
        {
            playerInputs.Player.Disable();
            shooting.onShootCooldown -= OnShootCooldown;
            PauseManager.onPauseChanged -= OnPauseChanged;
        }

        private void OnDestroy()
        {
            playerInputs.Player.Move.started -= OnMove;
            playerInputs.Player.Move.performed -= OnMove;
            playerInputs.Player.Move.canceled -= OnMove;

            playerInputs.Player.Dash.started -= OnDash;

            playerInputs.Player.Fire.started -= OnFire;
            playerInputs.Player.Fire.canceled -= OnFire;

            playerInputs.GameState.Pause.started -= OnPause;
            playerInputs.GameState.Retry.started -= OnRestart;
            playerInputs.GameState.Disable();
        }
    }
}
