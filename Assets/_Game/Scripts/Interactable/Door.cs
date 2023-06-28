using UnityEngine;
using UnityEngine.SceneManagement;

namespace TK
{
    public class Door : MonoBehaviour, IInteractable
    {
        public void interact()
        {
            SceneManager.LoadScene("NightScene");
        }
    }
}
