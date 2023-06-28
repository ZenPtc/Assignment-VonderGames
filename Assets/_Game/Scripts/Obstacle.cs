using UnityEngine;

namespace TK
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float dmgAmount = 5f;

        private void DoDamage(Player player)
        {
            player.TakeDamage(dmgAmount);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                DoDamage(player);
            }
        }
    }
}
