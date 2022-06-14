using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private int currentWeapon = 0; 

    // Start is called before the first frame update
    void Start()
    {
        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
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
        else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 2)
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
            if(i == currentWeapon)
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
