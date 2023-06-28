using UnityEngine;
using UnityEngine.InputSystem;

namespace TK
{
    public class PlayerParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _jumpParticle;
        [SerializeField] private ParticleSystem _dashParticle;
        [SerializeField] private ParticleSystem _shootParticle;
        [SerializeField] private Transform _jumpTransform;
        [SerializeField] private Transform _dashTransform;
        [SerializeField] private Transform _shootTransform;

        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _playerInputActions.Player.Jump.performed += DoJump;
            _playerInputActions.Player.Jump.Enable();

            _playerInputActions.Player.Dash.performed += DoDash;
            _playerInputActions.Player.Dash.Enable();

            _playerInputActions.Player.Attack.performed += DoAttack;
            _playerInputActions.Player.Attack.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Jump.performed -= DoJump;
            _playerInputActions.Player.Jump.Disable();

            _playerInputActions.Player.Dash.performed -= DoDash;
            _playerInputActions.Player.Dash.Disable();

            _playerInputActions.Player.Attack.performed -= DoAttack;
            _playerInputActions.Player.Attack.Disable();
        }

        private void DoJump(InputAction.CallbackContext ctx)
        {
            Instantiate(_jumpParticle, _jumpTransform);
        }
        private void DoDash(InputAction.CallbackContext ctx)
        {
            Instantiate(_dashParticle, _dashTransform);
        }
        private void DoAttack(InputAction.CallbackContext ctx)
        {
            Instantiate(_shootParticle, _shootTransform);
        }
    }
}
