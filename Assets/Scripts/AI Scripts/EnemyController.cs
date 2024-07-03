using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int _score;
    public int Score { get { return _score; } }
    private AudioController _audioController;

    private void Awake()
    {
        _audioController = AudioController.Instance;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_audioController.enemyHit);
        
    }

}
