using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    //Public Variables
    public enum WeaponType
    {
        NoWeapons,
        Pistol,
        Shotgun,
        Carbine
    }

    //public int maxAmmoCap;
    public AmmoHolder ammoHolder;
    public float shotDelay;
    public bool canShoot = true;
    public WeaponType Weapon { get { return _weaponType; } }

    //Private Variables
    [SerializeField] private WeaponType _weaponType = WeaponType.NoWeapons;
    [SerializeField] private GameObject _bullet;
    private Transform _muzzle;
    [SerializeField] private float shotgunSpread;
    private SceneController _sceneController;
    private AudioSource _audioSource;
    private AudioController _audioController;


    // Start is called before the first frame update
    void Start()
    {
        _sceneController = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneController>();
        canShoot = true;
        _muzzle = gameObject.transform.GetChild(0);
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_sceneController.isInMenu)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (ammoHolder.ammoCount > 0 && canShoot)
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
        
        Debug.Log("Bullet Shot! | Ammo remaining: " + ammoHolder.ammoCount);
        
        if(ammoHolder.ammoCount <= 0)
        {
            Debug.Log("Out of ammo!");
        }

    }

    IEnumerator CarbineShots()
    {
        canShoot = false;
        for (int i = 0; i < 3; i++)
        {
            if(ammoHolder.ammoCount > 0)
            {
                ammoHolder.ammoCount--;
                _audioSource.PlayOneShot(_audioController.carbineShot);
                Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
                yield return new WaitForSeconds(shotDelay/3);
            }
            
            
        }
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
        
    }

    IEnumerator PistolShots()
    {
        canShoot = false;
        Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
        ammoHolder.ammoCount--;
        _audioSource.PlayOneShot(_audioController.pistolShot);
        yield return new WaitForSeconds(shotDelay);
        canShoot = true; 
    }

    IEnumerator ShotgunShots()
    {
        canShoot = false;
        
        for(int i = 0; i < 5; i++)
        {
            float randomRangeX = Random.Range(-shotgunSpread, shotgunSpread);
            float randomRangeY = Random.Range(-shotgunSpread, shotgunSpread);
            Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation * Quaternion.Euler(randomRangeX, randomRangeY, 0)); // Aby ustawi? k?t rozrzutu, trzeba pomno?y? wyj?ciow? rotacj? razy now? rotacj? (jak przy wektorach)
        }
        ammoHolder.ammoCount--;
        _audioSource.PlayOneShot(_audioController.shotgunShot);
        _audioSource.PlayDelayed(0.6f);
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    

}
