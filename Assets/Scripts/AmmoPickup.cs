using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int amount;
    [SerializeField] private AmmoHolder _gunAmmoHolder;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<AmmoController>().ChangeAmmo(_gunAmmoHolder, amount);
            Destroy(gameObject);
        }
    }
}
