using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //Public Variables
    public GameObject enemyPrefab;
    public float spawnInterval;

    //Private Variables
    [SerializeField] private float _spawnDelay;


    // Start is called before the first frame update
    void OnEnable()
    {
        InvokeRepeating(nameof(SpawnEnemy), _spawnDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (gameObject.activeInHierarchy)
        {
            Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        }
        
    }
    
}
