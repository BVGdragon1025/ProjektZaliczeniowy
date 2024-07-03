using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class WeaponCarbine : Weapon
{
    public override IEnumerator ShootBullet()
    {
        canShoot = false;
        for(int i = 0; i < 3; i++)
        {
            if(ammoHolder.ammoCount > 0)
            {
                ammoHolder.ammoCount--;
                audioSource.PlayOneShot(AudioController.Instance.carbineShot);
                GetPooledBullet();
                yield return new WaitForSeconds(ShotDelay / 3);
            }

        }

        yield return new WaitForSeconds(ShotDelay);
        canShoot = true;
    }
}
