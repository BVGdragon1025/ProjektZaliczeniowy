using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _pistolAmmoCounter;
    [SerializeField] TextMeshProUGUI _shotgunAmmoCounter;
    [SerializeField] TextMeshProUGUI _carbineAmmoCounter;
    [SerializeField] TextMeshProUGUI _healthCounter;
    [SerializeField] GameObject _pistolCrosshair;
    [SerializeField] GameObject _shotgunCrosshair;
    [SerializeField] GameObject _carbineCrosshair;

    // Start is called before the first frame update
    void Start()
    {
        _healthCounter.gameObject.SetActive(true);
    }

    public void DisplayHealth(int healthAmount)
    {
        _healthCounter.text = healthAmount.ToString();
        
        if(healthAmount >= 75)
        {
            _healthCounter.color = Color.green;

        }
        else if(healthAmount < 75 && healthAmount >= 50)
        {
            _healthCounter.color = Color.yellow;
        }
        else if(healthAmount < 50 && healthAmount >= 25)
        {
            Color orange = new Color32(255, 127, 39, 255);
            _healthCounter.color = orange;
        }
        else
        {
            _healthCounter.color = Color.red;
        }
        
    }

    public void DisplayAmmoHUD(int weaponType, bool enabled, AmmoHolder weaponAmmoHolder)
    {
        switch (weaponType)
        {
            case 0:
                _pistolAmmoCounter.gameObject.SetActive(enabled);
                _pistolAmmoCounter.gameObject.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                break;
            case 1:
                _shotgunAmmoCounter.gameObject.SetActive(enabled);
                _shotgunAmmoCounter.gameObject.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                break;
            case 2:
                _carbineAmmoCounter.gameObject.SetActive(enabled);
                _carbineAmmoCounter.gameObject.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                break;
            default:
                Debug.Log("Something went wrong with Ammo Display!");
                break;
        }
    }


    public void DisplayCrosshair(int weaponType, bool enabled)
    {
        switch (weaponType)
        {
            case 0:
                _pistolCrosshair.gameObject.SetActive(enabled);
                break;
            case 1:
                _shotgunCrosshair.gameObject.SetActive(enabled);
                break;
            case 2:
                _carbineCrosshair.gameObject.SetActive(enabled);
                break;
            default:
                Debug.Log("Something went wrong with Crosshair Display!");
                break;
        }
    }

    public void UpdateAmmoDisplay(int weaponType, AmmoHolder weaponAmmoHolder)
    {

        switch (weaponType)
        {
            case 0:
                _pistolAmmoCounter.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                ChangeAmmoDisplayColor(_pistolAmmoCounter.GetComponent<TextMeshProUGUI>(), weaponAmmoHolder);
                break;
            case 1:
                _shotgunAmmoCounter.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                ChangeAmmoDisplayColor(_shotgunAmmoCounter.GetComponent<TextMeshProUGUI>(), weaponAmmoHolder);
                break;
            case 2:
                _carbineAmmoCounter.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                ChangeAmmoDisplayColor(_carbineAmmoCounter.GetComponent<TextMeshProUGUI>(), weaponAmmoHolder);
                break;
            default:
                Debug.Log("Something went wrong with updating Ammo Count!");
                break;
        }

    }

    private void ChangeAmmoDisplayColor(TextMeshProUGUI ammoText, AmmoHolder ammoHolder)
    {
        if(ammoHolder.ammoCount <= 0)
        {
            ammoText.color = Color.red;
        }
        else
        {
            ammoText.color = Color.white;
        }
    }

}
