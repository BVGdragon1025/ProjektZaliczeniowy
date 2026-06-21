using System;
using System.Collections;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    //Public Variables
    public GameObject weapon;

    //Private Variables
    [SerializeField] private int _startUpAmmo;
    [SerializeField] private AmmoType _ammoType;
    private AmmoHolder _ammoHoldder;
    private WeaponSwitch _weaponSwitch;
    private AudioController _audioController;
    private AudioSource _audioSource;
    private SceneController _sceneController;

    public static event Action<int, bool> OnWeaponUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        _sceneController = SceneController.Instance;
        _ammoHoldder = weapon.GetComponent<Weapon>().ammoHolder;
        _weaponSwitch = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSwitch>();
        _audioController = AudioController.Instance;
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        _sceneController.StartCoroutine(_sceneController.ActivatePickups(_ammoType));
    }

    // Update is called once per frame
    void Update() => transform.RotateAround(gameObject.transform.position, Vector3.up, 15 * Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ammoHoldder.isWeaponUnlocked = true;
            _weaponSwitch.currentWeapon = (int)_ammoType;
            _weaponSwitch.SwitchWeapon();
            _ammoHoldder.ammoCount = _startUpAmmo;
        }
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (_ammoHoldder.isWeaponUnlocked)
        {
            _audioSource.PlayOneShot(_audioController.gunPickUp);
            OnWeaponUnlocked?.Invoke((int)_ammoType - 1, false);
            StopCoroutine(ShowUnlockText());
        }   
    }

    private void OnEnable() => StartCoroutine(ShowUnlockText());

    IEnumerator ShowUnlockText()
    {
        OnWeaponUnlocked?.Invoke((int)_ammoType - 1, true);
        yield return new WaitForSeconds(5f);
        OnWeaponUnlocked?.Invoke((int)_ammoType - 1, false);
        Debug.Log("Time's up!");
    }

}
