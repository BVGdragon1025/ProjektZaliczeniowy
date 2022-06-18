using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Private Variables
    public int damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 bulletDirection = -gameObject.transform.forward.normalized;
        gameObject.GetComponent<Rigidbody>().velocity = bulletDirection * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.magnitude > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthController>().ChangeHealth(-damage);
            Destroy(gameObject);


        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
