using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region SINGLETON
    public static ObjectPooler instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>(); 

        foreach(Pool pool in pools)
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                ObjectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, ObjectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        IPooledObjects pooledObjects = objectToSpawn.GetComponent<IPooledObjects>();       
        if (pooledObjects != null)
            pooledObjects.OnObjectSpawn();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    public GameObject SpawnBulletsFromPool(string tag, Vector3 position, Quaternion rotation, Bullet.User user, Vector3 direction)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist.");
            return null;
        }

        Debug.Log(poolDictionary[tag].Count + " Antes");
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        Debug.Log(poolDictionary[tag].Count + " medio");
        objectToSpawn.GetComponent<Bullet>().setUser(user);
        objectToSpawn.GetComponent<Bullet>().SetDirection(direction);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObjects pooledObjects = objectToSpawn.GetComponent<IPooledObjects>();
        if (pooledObjects != null)
            pooledObjects.OnObjectSpawn();

        poolDictionary[tag].Enqueue(objectToSpawn);

        Debug.Log(poolDictionary[tag].Count + " final");
        return objectToSpawn;
    }
}
