using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int maxAmmoCap;
    public float shotDelay;
    public bool _canShoot = true;
    public WeaponType Weapon { get { return _weaponType; } }

    //Private Variables
    [SerializeField] private int _currentAmmoCap;
    [SerializeField] private WeaponType _weaponType = WeaponType.NoWeapons;
    [SerializeField] private GameObject _bullet;
    
    private GameObject _muzzle;
    [SerializeField]private float shotgunSpread;

    // Start is called before the first frame update
    void Start()
    {
        _canShoot = true;
        _muzzle = GameObject.FindGameObjectWithTag("Muzzle");

        switch (_weaponType)
        {
            case WeaponType.Pistol:
                _currentAmmoCap = maxAmmoCap / 10;
                break;
            case WeaponType.Shotgun:
                _currentAmmoCap = maxAmmoCap / 5;
                
                break;
            case WeaponType.Carbine:
                _currentAmmoCap = maxAmmoCap / 5;
                break;
            default:
                _currentAmmoCap = maxAmmoCap;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (_currentAmmoCap > 0 && _canShoot)
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
            
            Debug.Log("Bullet Shot! | Ammo remaining: " + _currentAmmoCap);
            
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    IEnumerator CarbineShots()
    {
        _canShoot = false;
        for (int i = 0; i < 3; i++)
        {
            _currentAmmoCap--;
            Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
            yield return new WaitForSeconds(shotDelay/3);
            
        }
        yield return new WaitForSeconds(shotDelay);
        _canShoot = true;
        
    }

    IEnumerator PistolShots()
    {
        _canShoot = false;
        Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
        _currentAmmoCap--;
        yield return new WaitForSeconds(shotDelay);
        _canShoot = true; 
    }

    IEnumerator ShotgunShots()
    {
        _canShoot = false;
        
        for(int i = 0; i < 5; i++)
        {
            float randomRangeX = Random.Range(-shotgunSpread, shotgunSpread);
            float randomRangeY = Random.Range(-shotgunSpread, shotgunSpread);
            Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation * Quaternion.Euler(randomRangeX, randomRangeY, 0)); // Aby ustawi� k�t rozrzutu, trzeba pomno�y� wyj�ciow� rotacj� razy now� rotacj� (jak przy wektorach)
        }
        _currentAmmoCap--;
        yield return new WaitForSeconds(shotDelay);
        _canShoot = true;
    }

}
