using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    //Public Variables
    public int currentWeapon = 0;

    //Private Variables
    private bool _isUnlocked;
    private UIController _uIController;

    // Start is called before the first frame update
    void Start()
    {
        _uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;
        AmmoHolder ammoHolder = transform.GetChild(currentWeapon).GetComponent<WeaponController>().ammoHolder;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }

        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            currentWeapon = 2;
        }

        if(previousWeapon != currentWeapon)
        {
            SwitchWeapon();
        }

        _uIController.UpdateAmmoDisplay(currentWeapon, ammoHolder);

    }

    public void SwitchWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == currentWeapon && weapon.GetComponent<WeaponController>().ammoHolder.isWeaponUnlocked)
            {
                weapon.gameObject.SetActive(true);
                weapon.gameObject.GetComponent<WeaponController>().canShoot = true;
                _uIController.DisplayAmmoHUD(i, true, weapon.gameObject.GetComponent<WeaponController>().ammoHolder);
                _uIController.DisplayCrosshair(i, true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
                _uIController.DisplayAmmoHUD(i, false, weapon.gameObject.GetComponent<WeaponController>().ammoHolder);
                _uIController.DisplayCrosshair(i, false);
            }
            i++;
        }
    }

}
