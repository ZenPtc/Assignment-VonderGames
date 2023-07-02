using TMPro;
using UnityEngine;

namespace TK
{
    public class DialogueBox : MonoBehaviour
    {
        public TMP_Text Name;
        public TMP_Text Dialogue;
        public Animator Animator;

        private void Awake()
        {
            DialogueManager.Instance.SetDialogueBox(Name, Dialogue, Animator);
        }
    }
}
