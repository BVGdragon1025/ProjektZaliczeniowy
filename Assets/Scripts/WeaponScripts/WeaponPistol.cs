using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class WeaponPistol : Weapon
{

    // ABSTRACTION
    public override IEnumerator ShootBullet()
    {
        canShoot = false;
        GetPooledBullet();
        //Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
        ammoHolder.ammoCount--;
        audioSource.PlayOneShot(AudioController.Instance.pistolShot);
        yield return new WaitForSeconds(ShotDelay);
        canShoot = true;
    }


}
