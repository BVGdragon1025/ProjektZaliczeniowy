using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int _score;
    private SceneController _sceneController;

    private void Awake()
    {
        _sceneController = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneController>();
    }

    private void OnDestroy()
    {
        _sceneController.CountKill();
        _sceneController.playerScore += _score;

    }

}
