using UnityEngine;

namespace TK
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float _dmgAmount = 10f;
        [SerializeField] private float _repeatRate = 1f;
        private Player _player;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                _player = player;
                InvokeRepeating("DoDamage", 0, _repeatRate);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                _player = null;
                CancelInvoke("DoDamage");
            }
        }

        private void DoDamage()
        {
            _player.TakeDamage(_dmgAmount);
        }
    }
}
