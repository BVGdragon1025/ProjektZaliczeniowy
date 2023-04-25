using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : Weapon
{
    [SerializeField, Range(0f, 0.9f)]
    private float _shotgunSpread;
    public float Spread { get { return _shotgunSpread; } set { _shotgunSpread = value; } }

    // POLYMORPHISM
    public override IEnumerator ShootBullet()
    {
        canShoot = false;

        for(int i = 0; i < 5; i++)
        {
            float randomRangeX = Random.Range(-Spread, Spread);
            float randomRangeY = Random.Range(-Spread, Spread);
            Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation * Quaternion.Euler(randomRangeX, randomRangeY, 0));
        }
        ammoHolder.ammoCount--;
        audioSource.PlayOneShot(AudioController.Instance.shotgunShot);
        audioSource.PlayDelayed(0.6f);
        yield return new WaitForSeconds(ShotDelay);

        canShoot = true;
    }

}
