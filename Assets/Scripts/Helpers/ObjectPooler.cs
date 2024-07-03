using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //Private Variables
    [SerializeField]
    private int _amountToPool;
    [SerializeField, Tooltip("If checked, pooled object will be instantiated as Child of GameObject with this component.")]
    private bool _instantiateAsChild;

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
            if (_instantiateAsChild)
            {
                temp = Instantiate(pooledObject, transform);
            }
            else
            {
                temp = Instantiate(pooledObject);
            }

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
