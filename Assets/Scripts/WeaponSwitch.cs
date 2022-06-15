using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private int currentWeapon = 0;
    private bool _isUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        SwitchWeapon();
        _isUnlocked = gameObject.GetComponentInChildren<WeaponController>()._isUnlocked;
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(currentWeapon >= transform.childCount - 1 && _isUnlocked)
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
            if(currentWeapon <= 0 && _isUnlocked)
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
        else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && _isUnlocked)
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && _isUnlocked)
        {
            currentWeapon = 2;
        }

        if(previousWeapon != currentWeapon)
        {
            SwitchWeapon();
        }

    }

    void SwitchWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == currentWeapon && _isUnlocked)
            {
                weapon.gameObject.SetActive(true);
                weapon.gameObject.GetComponent<WeaponController>()._canShoot = true;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

}
