using UnityEngine;

namespace TK
{
    public class PlayerHealthSystem
    {
        private float _maxHealth;
        private float _currentHealth;

        public PlayerHealthSystem(float initialHealth)
        {
            _maxHealth = initialHealth;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float dmgAmount)
        {
            _currentHealth -= dmgAmount;
            if(_currentHealth <= 0) _currentHealth = 0;
            Debug.Log($"Take {dmgAmount}damage, Remain health: {_currentHealth}");
        }

        public bool IsDead()
        {
            return _currentHealth <= 0;
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
    }
}
