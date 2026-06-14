using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int amountOfHealth;
    private SceneController _sceneController;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _sceneController = SceneController.Instance;
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    private void Update() => transform.RotateAround(gameObject.transform.position, Vector3.up, 15 * Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.TryGetComponent(out HealthController healthController))
            {
                if(healthController.CurrentHealth < healthController.maxHealth)
                {
                    _audioSource.PlayOneShot(AudioController.Instance.healthPickUp);
                    healthController.ChangeHealth(amountOfHealth);
                    _sceneController.StartCoroutine(_sceneController.ActivateHealthPickup(gameObject));
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
