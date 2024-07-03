using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    //Public Variables
    public GameObject weapon;

    //Private Variables
    [SerializeField] private int startUpAmmo;
    [SerializeField] private GameObject _gunInfo;
    private AmmoHolder _ammoHoldder;
    private WeaponSwitch _weaponSwitch;
    private AudioController _audioController;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _ammoHoldder = weapon.GetComponent<Weapon>().ammoHolder;
        _weaponSwitch = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSwitch>();
        _audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(gameObject.transform.position, Vector3.up, 15 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ammoHoldder.isWeaponUnlocked = true;
            if(weapon.name == "Shotgun")
            {
                _weaponSwitch.currentWeapon = 1;
                _weaponSwitch.SwitchWeapon();
            }
            else if(weapon.name == "Carbine")
            {
                _weaponSwitch.currentWeapon = 2;
                _weaponSwitch.SwitchWeapon();
            }
            _ammoHoldder.ammoCount = startUpAmmo;
        }

        gameObject.SetActive(false);

    }

    private void OnDisable()
    {
        if (_ammoHoldder.isWeaponUnlocked)
        {
            _audioSource.PlayOneShot(_audioController.gunPickUp);
            _gunInfo.SetActive(false);
            StopCoroutine(ShowUnlockText());
            
        }   
        
    }

    private void OnEnable()
    {
        StartCoroutine(ShowUnlockText());
    }

    IEnumerator ShowUnlockText()
    {
        _gunInfo.SetActive(true);
        yield return new WaitForSeconds(5f);
        _gunInfo.SetActive(false);
        Debug.Log("Time's up!");
    }

}
