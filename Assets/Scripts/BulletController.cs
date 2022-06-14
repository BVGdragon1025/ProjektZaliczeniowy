using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Public Variables
    public int damage;
    public float speed;

    //Private Variables

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.forward * speed, ForceMode.Impulse); //Musi tak byæ
        //Jak dobrze rozumiem, gameObject.transform.forward bierze wektor do przodu na podstawie obiektu przypiêtego do skryptu
        //A da³em ujemny, bo Ÿle obróci³em kule xD
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else
        {
            other.gameObject.GetComponent<HealthController>().ChangeHealth(-damage);
            Destroy(gameObject);
        }
    }

}
