using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    //Public Variables
    public enum WeaponType
    {
        Pistol,
        Shotgun,
        Carbine
    }
    public float shotDelay;
    public bool canShoot;

    //Private Variables
    [SerializeField] private WeaponType _weaponType = WeaponType.Pistol;
    [SerializeField] private GameObject _bullet;
    private Transform _muzzle;
    [SerializeField] private float shotgunSpread;
    private AudioSource _audioSource;
    private AudioController _audioController;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioController = AudioController.Instance;
        _muzzle = gameObject.transform.GetChild(0);
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    public void Shoot()
    {
        if (canShoot)
        {
            switch (_weaponType)
            {
                case WeaponType.Carbine:
                    StartCoroutine(CarbineShots());
                    break;
                case WeaponType.Shotgun:
                    StartCoroutine(ShotgunShots());
                    break;
                default:
                    StartCoroutine(PistolShots());
                    break;
            }
        }
    }

    IEnumerator CarbineShots()
    {
        canShoot = false;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
            _audioSource.PlayOneShot(_audioController.carbineShot);
            yield return new WaitForSeconds(shotDelay / 3);

        }
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;

    }

    IEnumerator PistolShots()
    {
        canShoot = false;
        Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
        _audioSource.PlayOneShot(_audioController.pistolShot);
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    IEnumerator ShotgunShots()
    {
        canShoot = false;

        for (int i = 0; i < 5; i++)
        {
            float randomRangeX = Random.Range(-shotgunSpread, shotgunSpread);
            float randomRangeY = Random.Range(-shotgunSpread, shotgunSpread);
            Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation * Quaternion.Euler(randomRangeX, randomRangeY, 0)); // Aby ustawiæ k¹t rozrzutu, trzeba pomno¿yæ wyjœciow¹ rotacjê razy now¹ rotacjê (jak przy wektorach)
        }
        _audioSource.PlayOneShot(_audioController.shotgunShot);
        _audioSource.PlayDelayed(0.6f);
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

}
