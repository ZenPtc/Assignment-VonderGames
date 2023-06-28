using UnityEngine;

namespace TK
{
    public class Checkpoints : MonoBehaviour
    {
        private PlayerLifeSystem _playerLifeSystem;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                _playerLifeSystem = player.PlayerLifeSystem;
                _playerLifeSystem.SetSpawnPosition(transform.position);
            }
        }
    }
}
