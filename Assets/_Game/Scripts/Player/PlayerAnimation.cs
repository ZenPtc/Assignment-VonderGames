using System;
using UnityEngine;

namespace TK
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private PlayerController _playerController;
        private Player _player;

        public event Action OnAttack;
        public event Action OnRespawn;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _playerController.OnJump += Jump;
            _playerController.OnAttack += Attack;
        }

        private void OnDisable()
        {
            _playerController.OnJump -= Jump;
            _playerController.OnAttack -= Attack;
        }

        private void Update()
        {
            _animator.SetBool("IsMove", _player.State == Player.PlayerState.OnMove);
            _animator.SetBool("IsDash", _player.State == Player.PlayerState.OnDash);
            _animator.SetBool("IsGrounded", _playerController._isGrounded);
            _animator.SetBool("IsDead", _player.PlayerLifeSystem.IsDead);

            if(_player.State == Player.PlayerState.OnDamaged) _animator.SetTrigger("OnHurt");
        }

        private void Jump()
        {
            _animator.SetTrigger("OnJump");
        }

        private void Attack()
        {
            _animator.SetTrigger("OnAttack");
        }

        //method call on animation event
        private void PlayerAttack()
        {
            OnAttack.Invoke();
        }

        private void PlayerRespawn()
        {
            OnRespawn.Invoke();
        }
    }
}
