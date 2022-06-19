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
    void Start()
    {
        InvokeRepeating("SpawnEnemy", _spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        if (gameObject.activeInHierarchy)
        {
            Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        }
        
    }
    
}
