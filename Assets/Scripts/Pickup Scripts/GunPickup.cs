using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    //Public Variables
    public GameObject weapon;

    //Private Variables
    [SerializeField] private int startUpAmmo;
    private AmmoHolder _ammoHoldder;
    private WeaponSwitch _weaponSwitch;

    // Start is called before the first frame update
    void Start()
    {
        _ammoHoldder = weapon.GetComponent<WeaponController>().ammoHolder;
        _weaponSwitch = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSwitch>();
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
}
