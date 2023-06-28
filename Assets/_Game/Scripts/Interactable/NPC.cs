using System.Collections;
using UnityEngine;

namespace TK
{
    public class NPC : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _chatBox;
        [SerializeField] private float _chatTime = 0.5f;
        private Coroutine _coroutine;

        public void interact()
        {
            if(_coroutine == null) _coroutine = StartCoroutine(Chating());
        }

        private void OnDisable()
        {
            StopCoroutine(Chating());
        }

        private IEnumerator Chating()
        {
            _chatBox.SetActive(true);
            yield return new WaitForSeconds(_chatTime);
            _chatBox.SetActive(false);
            _coroutine = null;
        }
    }
}
