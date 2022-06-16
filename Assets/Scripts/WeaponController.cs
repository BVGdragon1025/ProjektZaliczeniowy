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
    public bool _canShoot = true;
    public WeaponType Weapon { get { return _weaponType; } }
    public GameObject ammoStats;
    public GameObject weaponCrosshair;

    //Private Variables
    [SerializeField] private WeaponType _weaponType = WeaponType.NoWeapons;
    [SerializeField] private GameObject _bullet;
    private GameObject _muzzle;
    [SerializeField] private float shotgunSpread;
    


    // Start is called before the first frame update
    void Start()
    {
        ammoStats.gameObject.SetActive(true);
        ammoStats.GetComponent<TextMeshProUGUI>().text = ammoHolder.ammoCount.ToString();
        weaponCrosshair.gameObject.SetActive(true);

        _canShoot = true;
        _muzzle = GameObject.FindGameObjectWithTag("Muzzle");

        if(ammoHolder.ammoCount <= 0)
        {
            ammoHolder.ammoCount = ammoHolder.maxAmmoCount / 5;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        ammoStats.GetComponent<TextMeshProUGUI>().text = ammoHolder.ammoCount.ToString();
    }

    void Shoot()
    {
        if (ammoHolder.ammoCount > 0 && _canShoot)
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
            
            Debug.Log("Bullet Shot! | Ammo remaining: " + ammoHolder.ammoCount);
            
        }
        if(ammoHolder.ammoCount <= 0)
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
            ammoHolder.ammoCount--;
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
        ammoHolder.ammoCount--;
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
        ammoHolder.ammoCount--;
        yield return new WaitForSeconds(shotDelay);
        _canShoot = true;
    }

    

}
