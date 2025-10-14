using System.Collections.Generic;

using UnityEngine;

public class PusherManager : MonoBehaviour
{
    public static PusherManager Instance;

    [Header("Pusher Settings")]
    public Pusher pusherPrefab;
    public int poolSize = 20;

    [Header("Spawn Settings")]
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public float leftLimit = -5;
    public float rightLimit = 5;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float spawnInterval = 3;

    private float spawnTimer;
    private Queue<Pusher> pusherPool = new Queue<Pusher>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
            SpawnOnBothSides();
            spawnTimer = 0;
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Pusher newPusher = Instantiate(pusherPrefab, transform);
            newPusher.gameObject.SetActive(false);
            pusherPool.Enqueue(newPusher);
        }
    }

    private Pusher GetPusher()
    {
        if (pusherPool.Count > 0)
        {
            return pusherPool.Dequeue();
        }
        return Instantiate(pusherPrefab, transform);
    }

    private void SpawnOnBothSides()
    {
        if (leftSpawnPoint != null)
        {
            SpawnPusher(leftSpawnPoint.position, false);
        }
        if (rightSpawnPoint != null)
        {
            SpawnPusher(rightSpawnPoint.position, true);
        }
    }

    private void SpawnPusher(Vector3 position, bool moveRight)
    {
        Pusher pusher = GetPusher();
        pusher.transform.position = position;
        pusher.Initialize(moveSpeed, moveRight, leftLimit, rightLimit);
        pusher.gameObject.SetActive(true);
    }

    public void RecyclePusher(Pusher pusher)
    {
        pusher.gameObject.SetActive(false);
        pusherPool.Enqueue(pusher);
    }
}
