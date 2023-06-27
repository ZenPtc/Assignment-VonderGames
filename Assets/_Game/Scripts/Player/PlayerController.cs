using UnityEngine;
using UnityEngine.InputSystem;

namespace TK
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _feetPosition;
        [SerializeField] private float _groundCheckCircle;
        private bool _isGrounded;

        private PlayerInputActions _playerInputActions;
        private InputAction _move;
        private Player _player;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _move = _playerInputActions.Player.Move;
            _move.Enable();

            _playerInputActions.Player.Jump.performed += DoJump;
            _playerInputActions.Player.Jump.Enable();

            _playerInputActions.Player.Dash.performed += DoDash;
            _playerInputActions.Player.Dash.Enable();

            _playerInputActions.Player.Attack.performed += DoAttack;
            _playerInputActions.Player.Attack.Enable();

            _playerInputActions.Player.Interact.performed += DoInteract;
            _playerInputActions.Player.Interact.Enable();
        }

        private void OnDisable()
        {
            _move.Disable();

            _playerInputActions.Player.Jump.performed -= DoJump;
            _playerInputActions.Player.Jump.Disable();

            _playerInputActions.Player.Dash.performed -= DoDash;
            _playerInputActions.Player.Dash.Disable();

            _playerInputActions.Player.Attack.performed -= DoAttack;
            _playerInputActions.Player.Attack.Disable();

            _playerInputActions.Player.Interact.performed -= DoInteract;
            _playerInputActions.Player.Interact.Disable();
        }

        private void FixedUpdate()
        {
            _player.Move(_move.ReadValue<float>());
        }

        private void Update()
        {
            _isGrounded = Physics2D.OverlapCircle(_feetPosition.position, _groundCheckCircle, _groundLayer);
        }

        private void DoJump(InputAction.CallbackContext ctx) { if(_isGrounded) _player.Jump(); }
        private void DoDash(InputAction.CallbackContext ctx) { _player.Dash(); }
        private void DoAttack(InputAction.CallbackContext ctx) { _player.Attack(); }
        private void DoInteract(InputAction.CallbackContext ctx) { _player.Interact(); }
    }
}
