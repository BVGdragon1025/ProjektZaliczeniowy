using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int _score;
    private SceneController _sceneController;
    private AudioController _audioController;

    private void Awake()
    {
        _sceneController = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneController>();
        _audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
    }

    private void OnDestroy()
    {
        _sceneController.CountKill();
        _sceneController.playerScore += _score;

    }

    private void OnDisable()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_audioController.enemyHit);
    }

}
