using UnityEngine;

namespace TK
{
    public abstract class Player : MonoBehaviour
    {
        public virtual void Move(float moveValue)
        {
            //Debug.Log("Move!! " + moveValue);
        }

        public virtual void Jump()
        {
            Debug.Log("Jump!!");
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
