using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AlienShoot : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    // Stack-base ObjectPool
    private IObjectPool<GameObject> objectPool;
    // throw an exception if we try to return an existing item,
    // already in the pool
    [SerializeField] private bool collectionCheck = true;
    // extra options to control the pool capacity and maximun size
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 100;

    private List<GameObject> spawnedObjects;
    private List<GameObject> toRelease;

    [SerializeField] private float timeToSpawn = 5.0f;
    private float currentTime = 0.0f;
    private bool pause;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = new ObjectPool<GameObject>(
                CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);

        spawnedObjects = new List<GameObject>();
        toRelease = new List<GameObject>();

        currentTime = timeToSpawn;
        pause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            if (currentTime <= 0.0f)
            {
                // Spawn object
                GameObject obj = objectPool.Get();
                obj.transform.position = (Vector2)transform.position + Vector2.left * transform.localScale.x * 0.5f;
                spawnedObjects.Add(obj);
                // reset the time
                currentTime = timeToSpawn;
            }
            currentTime -= Time.deltaTime;
        }

        RemoveDeadProjectiles();
    }

    public void RemoveDeadProjectiles()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj.transform.position.x < -30)
            {
                toRelease.Add(obj);
            }
        }
        foreach (GameObject obj in toRelease)
        {
            objectPool.Release(obj);
            spawnedObjects.Remove(obj);
        }
        toRelease.Clear();
    }

    public void ResumeShoot()
    {
        currentTime = timeToSpawn;
        pause = false;
    }
    public void PauseShoot()
    {
        pause = true;
    }

    public void ResetShoots()
    {
        currentTime = timeToSpawn;
        foreach (GameObject obj in spawnedObjects)
        {
            obj.SetActive(false);
            objectPool.Release(obj);
        }
        spawnedObjects.Clear();
        toRelease.Clear();
    }

    private GameObject CreateProjectile()
    {
        GameObject objectInstance = Instantiate(projectilePrefab);
        return objectInstance;
    }

    private void OnReleaseToPool(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
    }

    private void OnGetFromPool(GameObject pooledObject)
    {
        pooledObject.SetActive(true);
    }

    private void OnDestroyPooledObject(GameObject pooledObject)
    {
        Destroy(pooledObject);
    }

}
