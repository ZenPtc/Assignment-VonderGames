using UnityEngine;

namespace TK
{
    public class SpikeTrap : MonoBehaviour
    {
        [SerializeField] private GameObject _trap;
        [SerializeField] private Animator _animator;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                _trap.SetActive(false);
                _animator.gameObject.SetActive(true);
                _animator.SetBool("IsEnable", true);
            }
        }
    }
}
