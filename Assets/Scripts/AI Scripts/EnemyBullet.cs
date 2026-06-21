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
    private Collider _collider;
    private Vector3 _startPos;

    private void Awake() => _collider = GetComponent<Collider>();

    void Start() => _audioController = AudioController.Instance;

    private void OnEnable()
    {
        _startPos = transform.position;
        Vector3 bulletDirection = -gameObject.transform.forward.normalized;
        gameObject.GetComponent<Rigidbody>().linearVelocity = bulletDirection * _speed;
        StartCoroutine(TriggerDelay());
    }

    void Update()
    {
        if(Vector3.Distance(_startPos, transform.position) > _range)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
            Physics.IgnoreCollision(_collider, _collider);
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.TryGetComponent(out IHealth health))
                    health.ChangeHealth(-damage);

                if (other.TryGetComponent(out AudioSource audioSource))
                    audioSource.PlayOneShot(_audioController.enemyHit);
            }
            gameObject.SetActive(false);
        }
    }

    private void OnDisable() => _collider.isTrigger = false;

    IEnumerator TriggerDelay()
    {
        yield return new WaitForFixedUpdate();
        _collider.isTrigger = true;
    }
}
