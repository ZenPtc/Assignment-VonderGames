using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TK
{
    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _dialogue;
        [SerializeField] private Animator _animator;

        private PlayerInputActions _playerInputActions;
        private Queue<string> _sentenceQueue;

        public override void Awake()
        {
            base.Awake();

            _playerInputActions = new PlayerInputActions();
            _sentenceQueue = new Queue<string>();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            _playerInputActions.Player.NextDialogue.performed += DisplayNextSentence;
            _playerInputActions.Player.NextDialogue.Enable();

            _sentenceQueue.Clear();
            _animator.SetBool("IsOpen", true);

            foreach(string sentence in dialogue.sentences)
            {
                _sentenceQueue.Enqueue(sentence);
            }

            _name.text = dialogue.name.ToString();
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
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        private void EndDialogue()
        {
            _animator.SetBool("IsOpen", false);
            _playerInputActions.Player.NextDialogue.performed -= DisplayNextSentence;
            _playerInputActions.Player.NextDialogue.Disable();
        }

        private void DisplayNextSentence(InputAction.CallbackContext ctx)
        {
            DisplayNextSentence();
        }

        private IEnumerator TypeSentence(string sentence)
        {
            _dialogue.text = "";

            foreach(char letter in sentence)
            {
                _dialogue.text += letter;
                yield return null;
            }
        }
    }
}
