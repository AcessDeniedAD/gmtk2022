using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasePoolerManager
{
    protected List<GameObject> pooledObjects;
    protected Transform parent;
    protected GameObject child;
    public void InstantiatePooledObjectsIntoParent(GameObject child, GameObject parent, int number)
    {
        if (pooledObjects == null)
        {
            pooledObjects = new List<GameObject>();
        }
        this.parent = parent.transform;
        this.child = child;
        for (int i = 0; i < number; i++)
        {
            var go = GameObject.Instantiate(child, parent.transform);
            pooledObjects.Add(go);
            go.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        var pooledObject = pooledObjects.FirstOrDefault(x => x.activeSelf);
        if(pooledObject == null)
        {
            pooledObject = GameObject.Instantiate(child, parent.transform);
        }
        return pooledObject;
    }
    public void DisablePooledObject(GameObject gameObjectToDisable)
    {
        gameObjectToDisable.SetActive(false);
    }
}
