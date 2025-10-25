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
    public MatterData Data => MatterTierManager.Instance.tierData.CurrentTierData;

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
        StartCoroutine(MatterSpawner());
    }

    private IEnumerator MatterSpawner()
    {
        while (true)
        {
            if (activeMatter.Count < Data.maxActiveMatter)
            {
                SpawnMatter();
            }
            yield return new WaitForSeconds(Data.spawnInterval);
        }
    }

    public void SpawnMatter()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        Matter newMatter = Instantiate(matterPrefab, spawnPos, Quaternion.identity);
        newMatter.Initialize(Data);
        Vector3 dropperScale = matterParent.transform.localScale;
        float xScale = dropperScale.x * 0.05f * Data.scale;
        float yScale = dropperScale.y * 15 * Data.scale;
        activeMatter.Add(newMatter);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        BoxCollider2D collider = matterParent.GetComponent<BoxCollider2D>();
        Bounds bounds = collider.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(x, y, 0);
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

    public List<MatterData> GetMatterSaveData()
    {
        return new List<MatterData> { Data };
    }
}
