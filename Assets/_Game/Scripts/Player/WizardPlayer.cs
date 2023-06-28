using UnityEngine;

namespace TK
{
    public class WizardPlayer : Player
    {
        [SerializeField] private Transform _bulletSpawner;
        [SerializeField] private GameObject _bulletPrefab;

        public override void Attack()
        {
            GameObject bulletCopy = Instantiate(_bulletPrefab, _bulletSpawner.position, Quaternion.identity);
            if(transform.localScale.x < 0) bulletCopy.transform.Rotate(0, 180, 0);
        }
    }
}
