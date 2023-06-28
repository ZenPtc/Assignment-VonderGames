using UnityEngine;

namespace TK
{
    public class WizardPlayer : Player
    {
        [SerializeField] private Transform bulletSpawner;
        [SerializeField] private GameObject bulletPrefab;

        public override void Attack()
        {
            GameObject bulletCopy = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);
            if(transform.localScale.x < 0) bulletCopy.transform.Rotate(0, 180, 0);
        }
    }
}
