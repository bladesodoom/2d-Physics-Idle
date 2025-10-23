using System.Collections.Generic;

using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public static ElevatorManager Instance;

    [Header("Platform Settings")]
    public Elevator platformPrefab;
    public int poolSize = 20;

    [Header("Spawn & Movement")]
    public Transform[] spawnPoints;
    public float topY = 5f;
    public float moveSpeed = 1f;
    public float spawnInterval = 3f;

    private float spawnTimer;
    private Queue<Elevator> platformPool = new Queue<Elevator>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializePool();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnFromAllPoints();
            spawnTimer = 0f;
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Elevator newPlatform = Instantiate(platformPrefab, transform);
            newPlatform.topY = topY;
            newPlatform.gameObject.SetActive(false);
            platformPool.Enqueue(newPlatform);
        }
    }

    private Elevator GetPlatform()
    {
        if (platformPool.Count > 0)
            return platformPool.Dequeue();

        Elevator newPlatform = Instantiate(platformPrefab, transform);
        newPlatform.topY = topY;
        return newPlatform;
    }

    private void SpawnFromAllPoints()
    {
        foreach (Transform spawn in spawnPoints)
        {
            SpawnPlatformAt(spawn);
        }
    }

    public void SpawnPlatformAt(Transform spawn)
    {
        if (spawn == null) return;

        Elevator platform = GetPlatform();
        platform.transform.position = spawn.position;
        platform.Initialize(moveSpeed, topY);
        platform.gameObject.SetActive(true);
    }

    public void RecyclePlatform(Elevator platform)
    {
        platform.gameObject.SetActive(false);
        platformPool.Enqueue(platform);
    }

    public void SetSpawnInterval(float interval)
    {
        spawnInterval = Mathf.Max(0.1f, interval);
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = Mathf.Max(0.1f, speed);
    }
}
