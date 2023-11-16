using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Private Variables
    public int damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    private AudioController _audioController;

    // Start is called before the first frame update
    void Start()
    {
        _audioController = AudioController.Instance;

    }

    private void OnEnable()
    {
        Vector3 bulletDirection = -gameObject.transform.forward.normalized;
        gameObject.GetComponent<Rigidbody>().velocity = bulletDirection * _speed;

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.magnitude > _range)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthController>().ChangeHealth(-damage);
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(_audioController.enemyHit);
            gameObject.SetActive(false);


        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
