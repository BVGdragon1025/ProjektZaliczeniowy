using System.Collections;
using UnityEngine;

public class WeaponPistol : Weapon
{
    public override IEnumerator ShootBullet()
    {
        canShoot = false;
        GetPooledBullet();
        ammoHolder.ammoCount--;
        audioSource.PlayOneShot(AudioController.Instance.pistolShot);
        yield return new WaitForSeconds(ShotDelay);
        canShoot = true;
    }


}
