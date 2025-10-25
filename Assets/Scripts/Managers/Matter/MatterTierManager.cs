using UnityEngine;

public class MatterTierManager : MonoBehaviour
{
    public static MatterTierManager Instance { get; private set; }

    [Header("References")]
    public MatterManager matterManager;
    public MatterUpgradeManager upgradeManager;

    [Header("Tier Data")]
    public MatterTierData tierData;

    [Header("Scaling Settings")]
    public int totalTiers = 5;
    public float baseValue = 1f;
    public float valueGrowth = 1.01f;

    public float baseDamage = 5;
    public float damageDecay = 0.99f;

    public float baseScale = 2f;
    public float scaleDecay = 0.99f;

    public int baseMaxActive = 10;
    public int activeIncrement = 5;

    public float baseSpawnInterval = 5f;
    public float spawnIntervalDecay = 0.99f;
    public float minSpawnInterval = 0.5f;

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
        {
            tierData = new MatterTierData();
        }
        GenerateTiers();
    }

    private void GenerateTiers()
    {
        tierData.tierList.Clear();

        for (int i = 0; i < totalTiers; i++)
        {
            MatterData tier = new MatterData
            {
                tierIndex = i,
                baseValue = baseValue * Mathf.Pow(valueGrowth, i),
                damage = baseDamage * Mathf.Pow(damageDecay, i),
                scale = baseScale * Mathf.Pow(scaleDecay, i),
                maxActiveMatter = baseMaxActive + i * activeIncrement,
                spawnInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval * Mathf.Pow(spawnDecay, i))
            };

            tierData.tierList.Add(tier);
        }
    }

    public void PromoteTier()
    {
        if (tierData.TryAdvanceTier())
        {
            MatterData newTier = tierData.CurrentTierData;
            matterManager.ApplyUpgrades(newTier);
            upgradeManager.ResetUpgrades();
            Debug.Log($"[MatterTierManager] Advanced to Tier {tierData.currentTier + 1}");
        }
        else
        {
            Debug.Log("Already at highest Matter Tier!");
        }
    }
}
