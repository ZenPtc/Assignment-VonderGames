using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TK
{
    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        private PlayerInputActions _playerInputActions;
        private Queue<string> _sentenceQueue;

        public override void Awake()
        {
            base.Awake();

            _playerInputActions = new PlayerInputActions();
            _sentenceQueue = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Debug.Log("StartDialogue with " + dialogue.name);
            _playerInputActions.Player.NextDialogue.performed += DisplayNextSentence;
            _playerInputActions.Player.NextDialogue.Enable();

            _sentenceQueue.Clear();

            foreach(string sentence in dialogue.sentences)
            {
                _sentenceQueue.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        private void DisplayNextSentence()
        {
            if(_sentenceQueue.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = _sentenceQueue.Dequeue();
            Debug.Log(sentence);
        }

        private void EndDialogue()
        {
            Debug.Log("EndDialogue");
            _playerInputActions.Player.NextDialogue.performed -= DisplayNextSentence;
            _playerInputActions.Player.NextDialogue.Disable();
        }

        private void DisplayNextSentence(InputAction.CallbackContext ctx)
        {
            DisplayNextSentence();
        }
    }
}
