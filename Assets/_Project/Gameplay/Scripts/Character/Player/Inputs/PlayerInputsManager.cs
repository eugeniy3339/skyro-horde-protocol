using UnityEngine;
using UnityEngine.InputSystem;

namespace HordeProtocol.Gameplay
{

    public class PlayerInputsManager : MonoBehaviour
    {
        private Movement movement;
        private Dash dash;
        private Shooting shooting;

        private Horde playerInputs;

        private Vector2 lastMoveInput;

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
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movement.moveDir = input;

            if(input.magnitude > 0.1f)
                lastMoveInput = input;
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            dash.DashIfCanTo(lastMoveInput);
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            Vector2 mousePos = playerInputs.Player.Mouse.ReadValue<Vector2>();
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 dirToTheMousePos = worldMousePos - (Vector2)transform.position;

            shooting.FireIfCanTo(dirToTheMousePos);
        }

        private void OnEnable()
        {
            playerInputs.Enable();
        }

        private void OnDisable()
        {
            playerInputs.Disable();
        }
    }
}
