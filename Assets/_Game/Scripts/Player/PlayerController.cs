using System;
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

        private IInteractable _interactObject = null;
        private bool _isFacingRight = true;
        private bool _isOnDialogue;

        public bool _isGrounded { get; private set; }
        public event Action<float> OnMove;
        public event Action OnJump;
        public event Action OnDash;
        public event Action OnAttack;
        public event Action<IInteractable> OnInteract;

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

            DialogueManager.Instance.OnDialogue += OnDialogue;
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

            DialogueManager.Instance.OnDialogue -= OnDialogue;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IInteractable interactable))
            {
                _interactObject = interactable;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IInteractable interactable))
            {
                if(_interactObject == interactable) _interactObject = null;
            }
        }

        private void FixedUpdate()
        {
            if(_isOnDialogue) return;
            if(_player.State != Player.PlayerState.OnDash)
            {
                float moveValue = _move.ReadValue<float>();
                OnMove?.Invoke(moveValue);
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

        private void DoJump(InputAction.CallbackContext ctx)
        {
            if(_isOnDialogue) return;
            if(_isGrounded) OnJump?.Invoke();
        }

        private void DoDash(InputAction.CallbackContext ctx)
        {
            if(_isOnDialogue) return;
            if(_player.CanDash) OnDash?.Invoke();
        }

        private void DoAttack(InputAction.CallbackContext ctx)
        {
            if(_isOnDialogue) return;
            if(_player.State == Player.PlayerState.OnMove || _player.State == Player.PlayerState.None) OnAttack?.Invoke();
        }

        private void DoInteract(InputAction.CallbackContext ctx)
        {
            if(_isOnDialogue) return;
            if(_interactObject != null) OnInteract?.Invoke(_interactObject);
        }

        private void OnDialogue(bool isOpen) { _isOnDialogue = isOpen; }
    }
}
