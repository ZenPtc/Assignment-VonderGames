using UnityEngine;
using UnityEngine.SceneManagement;

namespace TK
{
    public class Door : MonoBehaviour, IInteractable
    {
        public enum Scene
        {
            DayScene,
            NightScene
        }

        [SerializeField] private Scene _scene;

        public void interact()
        {
            SceneManager.LoadScene(_scene.ToString());
        }
    }
}
