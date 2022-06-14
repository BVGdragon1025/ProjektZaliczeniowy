using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Public Variables
    public int damage;
    public float speed;
    public float range;

    //Private Variables


    // Start is called before the first frame update
    void Start()
    {
        Transform cameraTransform = Camera.main.transform;
        RaycastHit hit;

        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit)) //Jeœli promieñ uderzy³ w coœ
        {
            Vector3 bulletDirection = (hit.point + -gameObject.transform.position).normalized;
            gameObject.GetComponent<Rigidbody>().velocity = bulletDirection * speed;
        }
        else
        {
            //Dla przypadków kiedy gracz np. strzela w niebo
            Vector3 bulletDirection = -gameObject.transform.forward.normalized;
            gameObject.GetComponent<Rigidbody>().velocity = bulletDirection * speed;

        }

         //Musi tak byæ
        //Jak dobrze rozumiem, transform.forward bierze wektor do przodu na podstawie obiektu przypiêtego do skryptu
        //A da³em ujemny, bo Ÿle obróci³em kule xD
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameObject.transform.position.magnitude > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.CompareTag("Enemy"))
        {   
            other.gameObject.GetComponent<HealthController>().ChangeHealth(-damage);
            Destroy(gameObject);
            

        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
