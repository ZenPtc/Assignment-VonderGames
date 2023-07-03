using UnityEngine;

namespace TK
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.25f;
        [SerializeField] private float _lifetime = 2;

        private void Start()
        {
            Invoke("DestroyBullet", _lifetime);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * _speed);
        }

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    DestroyBullet();
        //}

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}
