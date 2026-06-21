using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Public Variables
    public float damage;
    public float speed;
    public float range;

    //Private Variables
    private AudioController _audioController;
    private Rigidbody _rb;
    private Collider _collider;
    private Vector3 _spawnPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start() => _audioController = AudioController.Instance;

    private void OnEnable()
    {
        _spawnPosition = transform.position;

        Transform cameraTransform = Camera.main.transform;
        
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit))
        {
            Vector3 bulletDirection = (hit.point + -gameObject.transform.position).normalized;
            _rb.linearVelocity = bulletDirection * speed;
        }
        else
        {
            //Dla przypadk�w kiedy gracz np. strzela w niebo
            Vector3 bulletDirection = -gameObject.transform.forward.normalized;
            _rb.linearVelocity = bulletDirection * speed;

        }

        StartCoroutine(TriggerDelay());

        //Musi tak by�
        //Jak dobrze rozumiem, transform.forward bierze wektor do przodu na podstawie obiektu przypi�tego do skryptu
        //A da�em ujemny, bo �le obr�ci�em kule xD
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(_spawnPosition, transform.position) > range)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
            Physics.IgnoreCollision(_collider, _collider);
        else
        {
            if (other.TryGetComponent(out IHealth health))
                if (other.CompareTag("Enemy"))
                    health.ChangeHealth(-damage);

            if (other.TryGetComponent(out EnemyEyes enemyEyes))
                enemyEyes.ChangeHealth(-damage * 1.5f);

            if(other.TryGetComponent(out AudioSource audioSource))
            {
                if (other.CompareTag("Enemy"))
                    audioSource.PlayOneShot(_audioController.enemyHit);
                if (other.CompareTag("EnemyEyes"))
                    audioSource.PlayOneShot(_audioController.enemyHitCrit);
            }

            if (other.TryGetComponent(out Rigidbody rb))
                rb.AddForceAtPosition(_rb.linearVelocity * 10, rb.transform.position);

            gameObject.SetActive(false);

        }
    }

    private void OnDisable() => _collider.isTrigger = false; 

    IEnumerator TriggerDelay()
    {
        yield return new WaitForFixedUpdate();
        _collider.isTrigger = true;
        Debug.Log($"{gameObject.name} is trigger!");
    }

}
