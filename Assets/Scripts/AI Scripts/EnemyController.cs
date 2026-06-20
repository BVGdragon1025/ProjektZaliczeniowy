using UnityEngine;

public class EnemyController : MonoBehaviour, IHealth
{
    [SerializeField] private int _score;
    private AudioSource _audioSource;
    private Rigidbody _rb;
    private SceneController _sceneController;

    public int Score { get { return _score; } }
    public float CurrentHealth { get; set; }
    [field: SerializeField]
    public float MaxHealth { get; set; }

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>(); 
    }

    private void Start() => _sceneController = SceneController.Instance;

    private void OnEnable() 
    { 
        CurrentHealth = MaxHealth;
        _rb.isKinematic = true;
    }

    private void OnDisable() => _audioSource.PlayOneShot(AudioController.Instance.enemyHit);

    public void ChangeHealth(float healthAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healthAmount, 0, MaxHealth);
        CheckRemainingHealth();
    }

    public void CheckRemainingHealth()
    {
        if (CurrentHealth <= 0)
            KillCharacter();
    }

    public void KillCharacter()
    {
        _sceneController.CountKill(_score);
        _rb.isKinematic = false;
        //_rb.AddForceAtPosition(-transform.forward * 10.0f, _rb.centerOfMass, ForceMode.Impulse);
        Invoke(nameof(DisableObject), 3.0f);
    }

    private void DisableObject() => gameObject.SetActive(false);

}
