using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnObstacle
{
    public GameObject obj;
    public int poolIndex;
}

public class Spawner : MonoBehaviour
{
    // Stack-base ObjectPool
    private IObjectPool<GameObject>[] objectPools;

    // throw an exception if we try to return an existing item,
    // already in the pool
    [SerializeField] private bool collectionCheck = true;
    // extra options to control the pool capacity and maximun size
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    // prefab to spawn
    [SerializeField] private GameObject obstacle0;
    [SerializeField] private GameObject obstacle1;
    [SerializeField] private GameObject obstacle2;

    // time to wait for the next object to be spawned
    [SerializeField] private float timeToSpawn = 5.0f;
    private float currentTime = 0.0f;

    List<SpawnObstacle> spawnedObjects;
    List<SpawnObstacle> toRelease;

    bool firstSpawn;
    Vector2 startSpawnPosition;
    Vector2 spawnPosition;

    void Awake()
    {
        objectPools = new IObjectPool<GameObject>[3];
        objectPools[0] = new ObjectPool<GameObject>(CreateObstacle0, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
        objectPools[1] = new ObjectPool<GameObject>(CreateObstacle1, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
        objectPools[2] = new ObjectPool<GameObject>(CreateObstacle2, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);

        spawnedObjects = new List<SpawnObstacle>();
        toRelease = new List<SpawnObstacle>();

        startSpawnPosition = new Vector2(30, -3.5f);

        ResetSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0.0f)
        {
            // Update the spawnPosition
            if (firstSpawn)
            {
                firstSpawn = false;
            }
            else
            {
                int yDelta = Random.Range(-1, 2) * 2;

                Vector2 newPosition = spawnPosition + new Vector2(0, yDelta);
                if (newPosition.y < -3) {
                    newPosition.y = -3;
                }
                if (newPosition.y > 1) {
                    newPosition.y = 1;
                }
                spawnPosition = newPosition;
            }

            // Spawn object
            int index = Random.Range(0, 3);
            GameObject obj = objectPools[index].Get();
            SpawnObstacle obstacle = new SpawnObstacle();
            obstacle.obj = obj;
            obstacle.poolIndex = index;
            spawnedObjects.Add(obstacle);
            // reset the time
            currentTime = timeToSpawn;
        }
        currentTime -= Time.deltaTime;


        RemoveDeadObstacles();
    }

    public void RemoveDeadObstacles()
    {
        foreach (SpawnObstacle obstacle in spawnedObjects)
        {
            if (obstacle.obj.transform.position.x < -30)
            {
                toRelease.Add(obstacle);
            }
        }

        foreach (SpawnObstacle obstacle in toRelease)
        {
            objectPools[obstacle.poolIndex].Release(obstacle.obj);
            spawnedObjects.Remove(obstacle);
        }
        toRelease.Clear();
    }

    public void ResetSpawner()
    {
        firstSpawn = true;
        spawnPosition = startSpawnPosition;
        currentTime = timeToSpawn;

        foreach (SpawnObstacle obstacle in spawnedObjects)
        {
            obstacle.obj.SetActive(false);
            objectPools[obstacle.poolIndex].Release(obstacle.obj);
        }
        spawnedObjects.Clear();
        toRelease.Clear();
    }

    private GameObject CreateObstacle0()
    {
        GameObject objectInstance = Instantiate(obstacle0);
        objectInstance.transform.position = spawnPosition;
        return objectInstance;
    }

    private GameObject CreateObstacle1()
    {
        GameObject objectInstance = Instantiate(obstacle1);
        objectInstance.transform.position = spawnPosition;
        return objectInstance;
    }
    private GameObject CreateObstacle2()
    {
        GameObject objectInstance = Instantiate(obstacle2);
        objectInstance.transform.position = spawnPosition;
        return objectInstance;
    }

    private void OnReleaseToPool(GameObject pooledObject)
    {
        pooledObject.transform.position = startSpawnPosition;
        pooledObject.SetActive(false);
    }

    private void OnGetFromPool(GameObject pooledObject)
    {
        pooledObject.transform.position = spawnPosition;
        pooledObject.SetActive(true);
    }

    private void OnDestroyPooledObject(GameObject pooledObject)
    {
        Destroy(pooledObject);
    }

}
