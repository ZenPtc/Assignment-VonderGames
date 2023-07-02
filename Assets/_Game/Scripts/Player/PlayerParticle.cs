using UnityEngine;

namespace TK
{
    public class PlayerParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _jumpParticle;
        [SerializeField] private ParticleSystem _dashParticle;
        [SerializeField] private ParticleSystem _attackParticle;
        [SerializeField] private Transform _jumpTransform;
        [SerializeField] private Transform _dashTransform;
        [SerializeField] private Transform _attackTransform;

        private PlayerController _playerController;
        private PlayerAnimation _playerAnimation;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        private void OnEnable()
        {
            _playerController.OnJump += PlayJumpParticle;
            _playerController.OnDash += PlayDashParticle;

            _playerAnimation.OnAttack += PlayAttackParticle;
        }

        private void OnDisable()
        {
            _playerController.OnJump -= PlayJumpParticle;
            _playerController.OnDash -= PlayDashParticle;

            _playerAnimation.OnAttack -= PlayAttackParticle;
        }

        private void PlayJumpParticle()
        {
            Instantiate(_jumpParticle, _jumpTransform.position, _jumpTransform.rotation);
        }

        private void PlayDashParticle()
        {
            Instantiate(_dashParticle, _dashTransform.position, _dashTransform.rotation);
        }

        private void PlayAttackParticle()
        {
            Instantiate(_attackParticle, _attackTransform.position, _attackTransform.rotation);
        }
    }
}
