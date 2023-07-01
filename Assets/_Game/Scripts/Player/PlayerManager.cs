using UnityEngine;

namespace TK
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private Transform _spawnPoint;

        public override void Awake()
        {
            base.Awake();

            if(FindObjectOfType<Player>() == null)
            {
                Instantiate(_playerObject, _spawnPoint.position, _spawnPoint.rotation);
            }
        }
    }
}
