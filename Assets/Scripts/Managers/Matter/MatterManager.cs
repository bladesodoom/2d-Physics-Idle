using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MatterManager : MonoBehaviour
{
    public static MatterManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Matter matterPrefab;
    [SerializeField] private Transform matterParent;

    [Header("Runtime Data")]
    public MatterData data => MatterTierManager.Instance.tierData.CurrentTierData;

    private readonly List<Matter> activeMatter = new();

    private float spawnTimer;

    public static event System.Action OnMatterSizeChanged;

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
        // Load or initialize base data
        data ??= new MatterData();
        StartCoroutine(MatterSpawner());
    }

    private IEnumerator MatterSpawner()
    {
        while (true)
        {
            if (activeMatter.Count < data.maxActiveMatter)
            {
                SpawnMatter();
            }
            yield return new WaitForSeconds(data.spawnInterval);
        }
    }

    public void SpawnMatter()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        Matter newMatter = Instantiate(matterPrefab, spawnPos, Quaternion.identity, matterParent);
        newMatter.Initialize(data);
        activeMatter.Add(newMatter);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-5f, 5f), 5f, 0f);
    }

    public void RemoveMatter(Matter matter)
    {
        if (activeMatter.Contains(matter))
        {
            activeMatter.Remove(matter);
            Destroy(matter.gameObject);
        }
    }

    public void ResetMatter()
    {
        foreach (var m in activeMatter)
            Destroy(m.gameObject);
        activeMatter.Clear();
    }

    public void ApplyUpgrades(MatterData newData)
    {
        data = newData;
    }

    public List<MatterData> GetMatterSaveData()
    {
        return new List<MatterData> { data };
    }

    public void UpdateAllMatterStats()
    {
        foreach (var matter in activeMatter)
        {
            matter.ApplyData(data);
        }
    }
}
