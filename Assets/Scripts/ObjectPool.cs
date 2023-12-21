using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    private void Start()
    {
        // Initiate pool
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            AddNewObjectToPool();
        }
    }

    public GameObject GetPooledObject()
    {
        // Search for available object and return it
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // If no objects are avaibale, add a new object to the pool and return it
        return AddNewObjectToPool();
    }

    private GameObject AddNewObjectToPool()
    {
        GameObject tmp;
        tmp = Instantiate(objectToPool);
        tmp.SetActive(false);
        pooledObjects.Add(tmp);

        return tmp;
    }
}
