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
    [SerializeField] private float _shotgunSpread;
    private AudioSource _audioSource;
    private AudioController _audioController;
    private ObjectPooler _pooler;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioController = AudioController.Instance;
        _muzzle = gameObject.transform.GetChild(0);
        _pooler = SelectCorrectBulletPool();
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    void GetPooledBullet()
    {
        GameObject pooledObject = _pooler.GetObjectPool();
        pooledObject.transform.parent = null;
        pooledObject.transform.SetPositionAndRotation(_muzzle.transform.position, _muzzle.transform.rotation);
        pooledObject.SetActive(true);

    }

    void GetPooledBullet(float spreadX, float spreadY)
    {
        GameObject pooledObject = _pooler.GetObjectPool();
        pooledObject.transform.parent = null;
        pooledObject.transform.SetPositionAndRotation(_muzzle.transform.position, _muzzle.transform.rotation * Quaternion.Euler(spreadX, spreadY, 0));
        pooledObject.SetActive(true);

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
            GetPooledBullet();
            _audioSource.PlayOneShot(_audioController.carbineShot);
            yield return new WaitForSeconds(shotDelay / 3);

        }
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;

    }

    IEnumerator PistolShots()
    {
        canShoot = false;
        GetPooledBullet();
        _audioSource.PlayOneShot(_audioController.pistolShot);
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    IEnumerator ShotgunShots()
    {
        canShoot = false;

        for (int i = 0; i < 5; i++)
        {
            float randomRangeX = Random.Range(-_shotgunSpread, _shotgunSpread);
            float randomRangeY = Random.Range(-_shotgunSpread, _shotgunSpread);
            GetPooledBullet(randomRangeX, randomRangeY);
            // Aby ustawiæ k¹t rozrzutu, trzeba pomno¿yæ wyjœciow¹ rotacjê razy now¹ rotacjê (jak przy wektorach)
        }
        _audioSource.PlayOneShot(_audioController.shotgunShot);
        _audioSource.PlayDelayed(0.6f);
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    ObjectPooler SelectCorrectBulletPool()
    {
        ObjectPooler pooler;

        if(gameObject.name == "AIPistol")
        {
            pooler = GameObject.FindGameObjectWithTag("PistolPool").GetComponent<ObjectPooler>();
            return pooler;
        }

        if(gameObject.name == "AIShotgun")
        {
            pooler = GameObject.FindGameObjectWithTag("ShotgunPool").GetComponent<ObjectPooler>();
            return pooler;
        }

        if(gameObject.name == "AICarbine")
        {
            pooler = GameObject.FindGameObjectWithTag("CarbinePool").GetComponent<ObjectPooler>();
            return pooler;
        }

        return null;

    }

}
