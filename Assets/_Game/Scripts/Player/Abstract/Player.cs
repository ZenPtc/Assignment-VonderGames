using System.Collections;
using UnityEngine;

namespace TK
{
    public abstract class Player : MonoBehaviour, IDamageable
    {
        protected Rigidbody2D rb;

        [Header("Properties")]
        [SerializeField] protected float maxHealth = 100f;
        public PlayerLifeSystem PlayerHealthSystem { get; protected set; }

        [Header("Movement")]
        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float jumpForce = 8f;
        [SerializeField] protected float dashForce = 15f;
        [SerializeField] protected float dashTime = 0.3f;
        [SerializeField] protected float dashCooldown = 0.1f;
        public bool CanDash { get; protected set; }
        public bool IsDashing { get; protected set; }

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
            rb = GetComponent<Rigidbody2D>();
            PlayerHealthSystem = new PlayerLifeSystem(maxHealth, transform.position, this);

            CanDash = true;
            IsDashing = false;
        }

        protected void OnDisable()
        {
            StopCoroutine(Dashing());
        }

        protected virtual IEnumerator Dashing()
        {
            Vector2 direction = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
            float originalGravity = rb.gravityScale;

            CanDash = false;
            IsDashing = true;
            rb.gravityScale = 0f;
            rb.velocity = direction * dashForce;
            yield return new WaitForSeconds(dashTime);
            rb.gravityScale = originalGravity;
            IsDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            CanDash = true;
        }

        public virtual void Move(float moveValue)
        {
            rb.velocity = new Vector2(moveValue * moveSpeed, rb.velocity.y);
        }

        public virtual void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        public virtual void Dash()
        {
            StartCoroutine(Dashing());
        }

        public virtual void Attack() { }

        public virtual void Interact(IInteractable interactObject)
        {
            interactObject.interact();
        }

        public virtual void TakeDamage(float dmgAmount)
        {
            PlayerHealthSystem.TakeDamage(dmgAmount);
        }
    }
}
