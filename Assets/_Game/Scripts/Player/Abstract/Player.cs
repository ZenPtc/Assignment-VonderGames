using UnityEngine;

namespace TK
{
    public abstract class Player : MonoBehaviour
    {
        protected Rigidbody2D rb;

        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float jumpForce = 8f;

        protected void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Move(float moveValue)
        {
            rb.AddForce(new Vector2(moveValue, 0) * moveSpeed, ForceMode2D.Force);
        }

        public virtual void Jump()
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        public virtual void Dash()
        {
            Debug.Log("Dash!!");
        }

        public virtual void Attack()
        {
            Debug.Log("Attack!!");
        }

        public virtual void Interact()
        {
            Debug.Log("Interact!!");
        }
    }
}
