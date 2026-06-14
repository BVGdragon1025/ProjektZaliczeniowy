using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int _score;
    public int Score { get { return _score; } }
    private AudioController _audioController;
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void Start() => _audioController = AudioController.Instance;

    private void OnDisable() => _audioSource.PlayOneShot(_audioController.enemyHit);

}
