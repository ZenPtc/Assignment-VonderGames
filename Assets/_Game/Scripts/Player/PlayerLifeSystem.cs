using UnityEngine;

namespace TK
{
    public class PlayerLifeSystem
    {
        private Player _player;
        private Vector3 _spawnPosition;

        private float _maxHealth;
        private float _currentHealth;

        public PlayerLifeSystem(float initialHealth, Vector3 spawnPosition, Player player)
        {
            _player = player;
            _spawnPosition = spawnPosition;

            _maxHealth = initialHealth;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float dmgAmount)
        {
            _currentHealth -= dmgAmount;
            Debug.Log($"Take {dmgAmount} damage, Remain health: {_currentHealth}");
            if(_currentHealth <= 0) Die();
        }

        public void SetSpawnPosition(Vector3 position)
        {
            _spawnPosition = position;
        }

        private void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }

        private void Die()
        {
            Debug.Log("Player Die");
            Respawn();
        }

        private void Respawn()
        {
            ResetHealth();
            _player.transform.position = _spawnPosition;
            Debug.Log("Player Respawned");
        }
    }
}
