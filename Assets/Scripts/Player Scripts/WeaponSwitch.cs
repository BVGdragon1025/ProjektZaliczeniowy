using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitch : MonoBehaviour
{
    //Public Variables
    public int currentWeapon = 0;

    //Private Variables
    private UIController _uIController;
    private PlayerInputActions _inputActions;

    private void Awake() => _inputActions = new PlayerInputActions();

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.WeponSlot1.performed += OnWeaponOneChosen;
        _inputActions.Player.WeponSlot2.performed += OnWeaponTwoChosen;
        _inputActions.Player.WeponSlot3.performed += OnWeaponThreeChosen;
    }

    void Start()
    {
        _uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;
        AmmoHolder ammoHolder = transform.GetChild(currentWeapon).GetComponent<Weapon>().ammoHolder;
        float scrollWheel = _inputActions.Player.ChangeWeapons.ReadValue<float>();

        if (scrollWheel > 0f)
        {
            if(currentWeapon >= transform.childCount - 1)
                currentWeapon = 0;
            else
                currentWeapon++;
        }

        if(scrollWheel < 0f)
        {
            if(currentWeapon <= 0)
                currentWeapon = transform.childCount - 1;
            else
                currentWeapon--;
        }

        if(previousWeapon != currentWeapon)
            SwitchWeapon();

        _uIController.UpdateAmmoDisplay(currentWeapon, ammoHolder);

    }

    private void OnDisable()
    {
        _inputActions.Player.WeponSlot1.performed -= OnWeaponOneChosen;
        _inputActions.Player.WeponSlot2.performed -= OnWeaponTwoChosen;
        _inputActions.Player.WeponSlot3.performed -= OnWeaponThreeChosen;
    }

    private void OnWeaponOneChosen(InputAction.CallbackContext context) 
    { 
        currentWeapon = 0; 
        SwitchWeapon();
    }

    private void OnWeaponTwoChosen(InputAction.CallbackContext context)
    {
        if (transform.childCount >= 2)
            currentWeapon = 1;
        SwitchWeapon();
    }

    private void OnWeaponThreeChosen(InputAction.CallbackContext context)
    {
        if (transform.childCount >= 3)
            currentWeapon = 2;
        SwitchWeapon();
    }

    public void SwitchWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == currentWeapon && weapon.GetComponent<Weapon>().ammoHolder.isWeaponUnlocked)
            {
                weapon.gameObject.SetActive(true);
                weapon.gameObject.GetComponent<Weapon>().canShoot = true;
                _uIController.DisplayAmmoHUD(i, true, weapon.gameObject.GetComponent<Weapon>().ammoHolder);
                _uIController.DisplayCrosshair(i, true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
                _uIController.DisplayAmmoHUD(i, false, weapon.gameObject.GetComponent<Weapon>().ammoHolder);
                _uIController.DisplayCrosshair(i, false);
            }
            i++;
        }
    }

}
