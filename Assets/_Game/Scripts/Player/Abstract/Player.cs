using System.Collections;
using UnityEngine;

namespace TK
{
    public abstract class Player : MonoBehaviour, IDamageable
    {
        public enum PlayerState
        {
            None,
            OnMove,
            OnDash
        }

        [Header("Properties")]
        [SerializeField] protected float maxHealth = 100f;
        
        [Header("Movement")]
        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float jumpForce = 8f;
        [SerializeField] protected float dashForce = 15f;
        [SerializeField] protected float dashTime = 0.3f;
        [SerializeField] protected float dashCooldown = 0.1f;

        public PlayerLifeSystem PlayerLifeSystem { get; protected set; }
        public PlayerState State { get; protected set; }
        public bool CanDash { get; protected set; }

        protected Rigidbody2D rb;
        protected float originalGravity;
        protected bool isDashing;

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
            rb = GetComponent<Rigidbody2D>();
            PlayerLifeSystem = new PlayerLifeSystem(maxHealth, transform.position, this);

            originalGravity = rb.gravityScale;
            State = PlayerState.None;
            isDashing = false;
            CanDash = true;
        }

        protected void OnDisable()
        {
            StopCoroutine(Dashing());
        }

        protected virtual IEnumerator Dashing()
        {
            Vector2 direction = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
            originalGravity = rb.gravityScale;

            CanDash = false;
            isDashing = true;
            rb.gravityScale = 0f;
            rb.velocity = direction * dashForce;
            yield return new WaitForSeconds(dashTime);
            rb.gravityScale = originalGravity;
            State = PlayerState.None;
            isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            CanDash = true;
        }

        protected virtual void CancelDashing()
        {
            StopCoroutine(Dashing());
            rb.gravityScale = originalGravity;
            State = PlayerState.None;
            isDashing = false;
            CanDash = true;
        }

        public virtual void Move(float moveValue)
        {
            if(moveValue == 0) State = PlayerState.None;
            else State = PlayerState.OnMove;
            rb.velocity = new Vector2(moveValue * moveSpeed, rb.velocity.y);
        }

        public virtual void Jump()
        {
            CancelDashing();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        public virtual void Dash()
        {
            State = PlayerState.OnDash;
            StartCoroutine(Dashing());
        }

        public virtual void Attack() { }

        public virtual void Interact(IInteractable interactObject)
        {
            interactObject.interact();
        }

        public virtual void TakeDamage(float dmgAmount)
        {
            PlayerLifeSystem.TakeDamage(dmgAmount);
        }
    }
}
