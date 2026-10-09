using UnityEngine;
using UnityEngine.InputSystem;

namespace HordeProtocol.Gameplay
{

    public class PlayerInputsManager : MonoBehaviour
    {
        private PlayerMovement movement;
        private Dash dash;

        private Horde playerInputs;

        private Vector2 lastMoveInput;

        private void Awake()
        {
            movement = GetComponent<PlayerMovement>();
            dash = GetComponent<Dash>();

            playerInputs = new Horde();

            playerInputs.Player.Move.started += OnMove;
            playerInputs.Player.Move.performed += OnMove;
            playerInputs.Player.Move.canceled += OnMove;

            playerInputs.Player.Dash.started += OnDash;
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
