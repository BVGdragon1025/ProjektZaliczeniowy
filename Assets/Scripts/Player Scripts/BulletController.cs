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
    private AudioController _audioController;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BoxCollider _boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _audioController = AudioController.Instance;

    }

    private void OnEnable()
    {
        Transform cameraTransform = Camera.main.transform;
        
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit)) //Je�li promie� uderzy� w co�
        {
            Vector3 bulletDirection = (hit.point + -gameObject.transform.position).normalized;
            _rb.velocity = bulletDirection * speed;
        }
        else
        {
            //Dla przypadk�w kiedy gracz np. strzela w niebo
            Vector3 bulletDirection = -gameObject.transform.forward.normalized;
            _rb.velocity = bulletDirection * speed;

        }

        StartCoroutine(TriggerDelay());

        //Musi tak by�
        //Jak dobrze rozumiem, transform.forward bierze wektor do przodu na podstawie obiektu przypi�tego do skryptu
        //A da�em ujemny, bo �le obr�ci�em kule xD
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameObject.transform.position.magnitude > range)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
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

    private void OnDisable()
    {
        _boxCollider.isTrigger = false;
    }

    IEnumerator TriggerDelay()
    {
        yield return new WaitForFixedUpdate();
        _boxCollider.isTrigger = true;
        Debug.Log($"{gameObject.name} is trigger!");
    }

}
