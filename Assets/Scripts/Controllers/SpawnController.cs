using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]

public class SpawnController : MonoBehaviour
{
    //Public Variables
    public GameObject enemyPrefab;
    public float spawnInterval;

    //Private Variables
    [SerializeField] private float _spawnDelay;
    private ObjectPooler _pooler;

    private void Awake()
    {
        _pooler = GetComponent<ObjectPooler>();
        _pooler.pooledObject = enemyPrefab;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        InvokeRepeating(nameof(SpawnEnemy), _spawnDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        GameObject enemy = _pooler.GetObjectPool();

        if (gameObject.activeInHierarchy)
        {
            if(enemy != null)
            {
                enemy.transform.SetPositionAndRotation(transform.parent.position, Quaternion.identity);
                enemy.SetActive(true);

            }
            //Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        }
        
    }
    
}
