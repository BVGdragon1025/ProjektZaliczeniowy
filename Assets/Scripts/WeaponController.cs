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

    public int maxAmmoCap;
    public float shotDelay;
    public bool _canShoot = true;
    public bool _isUnlocked;
    public WeaponType Weapon { get { return _weaponType; } }

    //Private Variables
    [SerializeField] private int _currentAmmoCap;
    [SerializeField] private WeaponType _weaponType = WeaponType.NoWeapons;
    [SerializeField] private GameObject _bullet;
    private GameObject _muzzle;
    [SerializeField] private float shotgunSpread;
    [SerializeField] private GameObject ammoStats;

    private void Awake()
    {
        foreach(WeaponController controller in GetComponentsInChildren<WeaponController>())
        {
            if (controller._isUnlocked)
            {
                ammoStats.gameObject.SetActive(true);
            }
            else
            {
                ammoStats.gameObject.SetActive(false);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ammoStats.GetComponent<TextMeshProUGUI>().text = _currentAmmoCap.ToString();

        _canShoot = true;
        _muzzle = GameObject.FindGameObjectWithTag("Muzzle");

        switch (_weaponType)
        {
            case WeaponType.Pistol:
                _currentAmmoCap = maxAmmoCap / 5;
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
        ammoStats.GetComponent<TextMeshProUGUI>().text = _currentAmmoCap.ToString();
    }

    void Shoot()
    {
        if (_currentAmmoCap > 0 && _canShoot)
        {
            ammoStats.GetComponent<TextMeshProUGUI>().color = Color.white;
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
        if(_currentAmmoCap <= 0)
        {
            ammoStats.GetComponent<TextMeshProUGUI>().color = Color.red;
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
            Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation * Quaternion.Euler(randomRangeX, randomRangeY, 0)); // Aby ustawiæ k¹t rozrzutu, trzeba pomno¿yæ wyjœciow¹ rotacjê razy now¹ rotacjê (jak przy wektorach)
        }
        _currentAmmoCap--;
        yield return new WaitForSeconds(shotDelay);
        _canShoot = true;
    }

}
