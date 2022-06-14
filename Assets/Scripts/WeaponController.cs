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
    public WeaponType Weapon { get { return _weaponType; } }

    //Private Variables
    [SerializeField] private int _currentAmmoCap;
    [SerializeField] private WeaponType _weaponType = WeaponType.NoWeapons;
    [SerializeField] private GameObject _bullet;
    private GameObject _muzzle;

    // Start is called before the first frame update
    void Start()
    {
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
        if (_currentAmmoCap > 0)
        {
            switch (_weaponType)
            {
                case WeaponType.Pistol:
                    Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
                    break;
            }
            _currentAmmoCap--;
            Debug.Log("Bullet Shot! | Ammo remaining: " + _currentAmmoCap);
            
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

}
