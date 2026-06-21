using System.Collections;
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
    private AudioSource _audioSource;
    private PlayerInputActions _inputActions;

    private void Awake() => _inputActions = new PlayerInputActions();

    private void OnEnable() => _inputActions.Player.Shoot.performed += OnShootPerformed;

    void Start()
    {
        canShoot = true;
        _muzzle = gameObject.transform.GetChild(0);
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnDisable() => _inputActions.Player.Shoot.performed -= OnShootPerformed;

    private void OnShootPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!SceneController.Instance.isInMenu)
            Shoot();
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
    }

    IEnumerator CarbineShots()
    {
        canShoot = false;
        for (int i = 0; i < 3; i++)
        {
            if(ammoHolder.ammoCount > 0)
            {
                ammoHolder.ammoCount--;
                _audioSource.PlayOneShot(AudioController.Instance.carbineShot);
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
        _audioSource.PlayOneShot(AudioController.Instance.pistolShot);
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
            Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation * Quaternion.Euler(randomRangeX, randomRangeY, 0)); // Aby ustawiæ k¹t rozrzutu, trzeba pomno¿yæ wyjœciow¹ rotacjê razy now¹ rotacjê (jak przy wektorach)
        }

        ammoHolder.ammoCount--;
        _audioSource.PlayOneShot(AudioController.Instance.shotgunShot);
        _audioSource.PlayDelayed(0.6f);
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }
}
