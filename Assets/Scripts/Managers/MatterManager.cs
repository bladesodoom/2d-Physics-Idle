using System.Collections.Generic;

using UnityEngine;

public class MatterManager : MonoBehaviour
{
    public static MatterManager Instance;

    [Header("Prefabs")]
    public Matter matterPrefab;
    public BoxCollider2D spawnArea;
    public int poolSize = 50;
    public int maxActiveMatter = 5;

    [Header("Spawn Settings")]
    public float spawnInterval = 1f;
    public float matterScale = 2f;

    [Header("Stats")]
    public float baseValue = 1f;
    public float matterDamage = 1f;

    public float currentValue = 1f;

    private int currentActiveMatter;
    private float spawnTimer;
    private Queue<Matter> matterPool = new Queue<Matter>();

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
        if (spawnTimer >= spawnInterval && currentActiveMatter < maxActiveMatter)
        {
            SpawnMatter();
            spawnTimer = 0f;
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Matter newMatter = Instantiate(matterPrefab, spawnArea.transform);
            Transform t = newMatter.transform;
            t.SetParent(this.transform);
            newMatter.gameObject.SetActive(false);
            matterPool.Enqueue(newMatter);
        }
    }

    public void SpawnMatter()
    {
        Vector2 randomPos = GetRandomPointInArea();

        Matter matter = GetAvailableMatter();
        matter.Initialize(baseValue, matterDamage);
        Transform t = matter.transform;
        t.SetParent(null);
        t.position = randomPos;
        t.localScale = new Vector3(matterScale, matterScale, 1);
        t.SetParent(transform, true);
        matter.gameObject.SetActive(true);
        currentActiveMatter++;

    }

    private Vector2 GetRandomPointInArea()
    {
        Bounds bounds = spawnArea.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(randomX, randomY);
    }

    private Matter GetAvailableMatter()
    {
        if (matterPool.Count > 0)
        {
            return matterPool.Dequeue();
        }
        else
        {
            return Instantiate(matterPrefab, spawnArea.transform);
        }
    }

    public void RecycleMatter(Matter matter)
    {
        matter.transform.position = new Vector3(0, 50, 0);
        currentActiveMatter--;
        matter.gameObject.SetActive(false);
        matterPool.Enqueue(matter);
    }

    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }

    public void IncreaseMaxActiveMatter()
    {
        maxActiveMatter += 5;
    }
}
