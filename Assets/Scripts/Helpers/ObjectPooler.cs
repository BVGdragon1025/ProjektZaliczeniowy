using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //Private Variables
    [SerializeField]
    private int _amountToPool;

    //Public variables
    public List<GameObject> objectsTable;
    public GameObject pooledObject;

    // Start is called before the first frame update
    void Start()
    {
        objectsTable = new();
        SetObjectPool();
    }

    private void SetObjectPool()
    {
        GameObject temp;

        for(int i = 0; i < _amountToPool; i++)
        {
            temp = Instantiate(pooledObject, transform);
            objectsTable.Add(temp);
            objectsTable[i].SetActive(false);
        }
    }

    public GameObject GetObjectPool()
    {
        for(int i = 0; i < _amountToPool; i++)
        {
            if (!objectsTable[i].activeInHierarchy)
            {
                return objectsTable[i];
            }
            
        }
        return null;
    }

}
