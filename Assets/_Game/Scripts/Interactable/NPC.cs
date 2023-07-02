using UnityEngine;

namespace TK
{
    public class NPC : MonoBehaviour, IInteractable
    {
        public enum Name{
            Shiro
        }

        [SerializeField] private Dialogue _dialogue;

        public void interact()
        {
            DialogueManager.Instance.StartDialogue(_dialogue);
        }
    }
}
