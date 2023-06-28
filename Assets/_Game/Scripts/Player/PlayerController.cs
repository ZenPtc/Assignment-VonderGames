using UnityEngine;
using UnityEngine.InputSystem;

namespace TK
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _feetPosition;
        [SerializeField] private float _groundCheckCircle;

        private PlayerInputActions _playerInputActions;
        private InputAction _move;
        private Player _player;

        private bool _isFacingRight = true;
        private bool _isGrounded;

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
            if(!_player.IsDashing)
            {
                float moveValue = _move.ReadValue<float>();
                _player.Move(moveValue);
                PlayerFacingControl(moveValue);
            }
        }

        private void Update()
        {
            _isGrounded = Physics2D.OverlapCircle(_feetPosition.position, _groundCheckCircle, _groundLayer);
        }

        private void PlayerFacingControl(float moveValue)
        {
            if(_isFacingRight && moveValue < 0 || !_isFacingRight && moveValue > 0)
            {
                _isFacingRight = !_isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }

        private void DoJump(InputAction.CallbackContext ctx) { if(_isGrounded) _player.Jump(); }
        private void DoDash(InputAction.CallbackContext ctx) { if(_player.CanDash) _player.Dash(); }
        private void DoAttack(InputAction.CallbackContext ctx) { _player.Attack(); }
        private void DoInteract(InputAction.CallbackContext ctx) { _player.Interact(); }
    }
}
