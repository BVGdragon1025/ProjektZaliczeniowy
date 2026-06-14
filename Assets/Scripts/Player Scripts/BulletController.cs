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
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _boxCollider;

    // Start is called before the first frame update
    void Start() => _audioController = AudioController.Instance;

    private void OnEnable()
    {
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
        if (gameObject.transform.position.magnitude > range)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                if (other.gameObject.TryGetComponent(out HealthController healthController))
                    healthController.ChangeHealth(-damage);
                if(other.gameObject.TryGetComponent(out AudioSource audioSource))
                    audioSource.PlayOneShot(_audioController.enemyHit);
                gameObject.SetActive(false);
                break;
            case "EnemyEyes":
                other.gameObject.GetComponentInParent<HealthController>().ChangeHealth(-damage * 1.5f);
                other.gameObject.GetComponentInParent<AudioSource>().PlayOneShot(_audioController.enemyHitCrit);
                gameObject.SetActive(false);
                break;
            case "Bullet":
                Physics.IgnoreCollision(_boxCollider, _boxCollider);
                break;
            default:
                gameObject.SetActive(false);
                break;

        }
    }

    private void OnDisable() => _boxCollider.isTrigger = false; 

    IEnumerator TriggerDelay()
    {
        yield return new WaitForFixedUpdate();
        _boxCollider.isTrigger = true;
        Debug.Log($"{gameObject.name} is trigger!");
    }

}
