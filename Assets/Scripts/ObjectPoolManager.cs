using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObject> objectPools = new List<PooledObject>();

    private GameObject _objectPoolEmptyHolder;
    private static GameObject _enemiesEmpty;
    private static GameObject _audioEmpty;
    private static GameObject _abilitiesEmpty;
    [SerializeField] private GameObject ui;
    private static GameObject _UIMain;

    public enum PoolType
    {
        Enemies,
        Abilities,
        UI,
        Audio,
        None
    }

    public static PoolType poolType;

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _abilitiesEmpty = new GameObject("Pooled Abilities");
        _abilitiesEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _enemiesEmpty = new GameObject("Pooled Enemies");
        _enemiesEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _audioEmpty = new GameObject("Audio");
        _audioEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _UIMain = ui;
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObject pool = objectPools.Find(p => p.name == objectToSpawn.name);

        if(pool == null)
        {
            pool = new PooledObject() { name = objectToSpawn.name };
            objectPools.Add(pool);
        }

        GameObject spawnableObj = null;
        foreach(GameObject obj in pool.pooledObjects)
        {
            if(obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }   

        if(spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);

            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if(parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.pooledObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void RemoveObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        PooledObject pool = objectPools.Find(p => p.name == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release pool object that is not poolable" + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.pooledObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch(poolType)
        {
            case PoolType.Abilities:
                return _abilitiesEmpty;
            case PoolType.Enemies:
                return _enemiesEmpty;
            case PoolType.UI:
                return _UIMain;
            case PoolType.Audio:
                return _audioEmpty;
            case PoolType.None:
                return null;
            default:
                return null;

        }
    }
}

public class PooledObject
{
    public string name;
    public List<GameObject> pooledObjects = new List<GameObject>();
}
