using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    //Public Variables
    public enum PickupTypes
    {
        Health,
        PistolAmmo,
        ShotgunAmmo,
        CarbineAmmo,
        Gun
    }

    //Private Variables
    [SerializeField] private PickupTypes _type;
    [SerializeField] private int amount;
    private HealthController _playerHealthController;
    private WeaponController _playerPistolController;
    private WeaponController _playerShotgunController;
    private WeaponController _playerCarbineController;


    // Start is called before the first frame update
    void Start()
    {
        _playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
        _playerPistolController = GameObject.FindGameObjectWithTag("GunPistol").GetComponent<WeaponController>();
        _playerShotgunController = GameObject.FindGameObjectWithTag("GunShotgun").GetComponent<WeaponController>();
        _playerCarbineController = GameObject.FindGameObjectWithTag("GunCarbine").GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (_type)
        {
            case PickupTypes.Health:
                if(other.tag == "Player" && other.gameObject.GetComponent<HealthController>().CurrentHealth < 100)
                {
                    _playerHealthController.ChangeHealth(amount);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Player has full health!");
                }
                break;
            case PickupTypes.PistolAmmo:
                if(other.tag == "Player" && _playerPistolController.CurrentAmmo < _playerPistolController.maxAmmoCap)
                {
                    _playerPistolController.ChangeAmmo(amount);
                    Debug.Log("Current pistol ammo count: " + _playerPistolController.CurrentAmmo);
                    Destroy(gameObject);
                }
                break;
            case PickupTypes.ShotgunAmmo:
                if(other.tag == "Player" && _playerShotgunController.CurrentAmmo < _playerShotgunController.maxAmmoCap)
                {
                    _playerShotgunController.ChangeAmmo(amount);
                    Debug.Log("Current shotgun ammo count: " + _playerShotgunController.CurrentAmmo);
                    Destroy(gameObject);
                }
                break;
            case PickupTypes.CarbineAmmo:
                if(other.tag == "Player" && _playerCarbineController.CurrentAmmo < _playerCarbineController.maxAmmoCap)
                {
                    _playerCarbineController.ChangeAmmo(amount);
                    Debug.Log("Current carbine ammo count: " + _playerCarbineController.CurrentAmmo);
                    Destroy(gameObject);
                }
                break;
            
        }

    }
}
