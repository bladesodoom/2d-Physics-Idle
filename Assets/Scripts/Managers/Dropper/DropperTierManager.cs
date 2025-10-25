using UnityEngine;

public class DropperTierManager : MonoBehaviour
{
    public static DropperTierManager Instance { get; private set; }

    [Header("References")]
    public DropperManager dropperManager;
    public DropperUpgradeManager upgradeManager;

    [Header("Tier Data")]
    public DropperTierData tierData;

    [Header("Scaling Settings")]
    public int totalTiers = 10;

    [Header("Base Values")]
    public float baseSpawnInterval = 2.0f;
    public float spawnIntervalDecay = 0.9f;

    public int baseSpawnCount = 1;
    public int spawnCountGrowth = 1;

    public float baseCooldown = 5f;
    public float cooldownDecay = 0.9f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (tierData == null)
            tierData = new DropperTierData();

        GenerateTiers();
    }

    private void GenerateTiers()
    {
        tierData.tierList.Clear();

        for (int i = 0; i < totalTiers; i++)
        {
            DropperData tier = new DropperData
            {
                tierIndex = i,
                spawnInterval = baseSpawnInterval * Mathf.Pow(spawnIntervalDecay, i),
                spawnCount = baseSpawnCount + (i * spawnCountGrowth),
                cooldownTime = baseCooldown * Mathf.Pow(cooldownDecay, i)
            };

            tierData.tierList.Add(tier);
        }
    }

    public void PromoteTier()
    {
        if (tierData.TryAdvanceTier())
        {
            DropperData newTier = tierData.CurrentTierData;
            dropperManager.ApplyTierData(newTier);
            upgradeManager.ResetUpgrades();
            Debug.Log($"[DropperTierManager] Advanced to Tier {tierData.currentTier + 1}");
        }
        else
        {
            Debug.Log("Already at highest Dropper Tier!");
        }
    }
}
