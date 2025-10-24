using UnityEngine;

public class MatterTierManager : MonoBehaviour
{
    public static MatterTierManager Instance { get; private set; }

    [Header("References")]
    public MatterManager matterManager;
    public MatterUpgradeManager upgradeManager;

    [Header("Tier Data")]
    public MatterTierData tierData;

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
            InitializeDefaultTiers();
        }
    }

    private void InitializeDefaultTiers()
    {
        // Example: create 3 tiers
        for (int i = 0; i < 3; i++)
        {
            MatterData tier = new MatterData
            {
                baseValue = 1f * Mathf.Pow(2f, i),
                damage = 5f * Mathf.Pow(1.5f, i),
                scale = 1f / Mathf.Pow(1.1f, i),
                maxActiveMatter = 10 + (i * 5),
                spawnInterval = Mathf.Max(0.5f, 1.5f - i * 0.3f)
            };
            tierData.tierList.Add(tier);
        }
    }

    public void PromoteTier()
    {
        if (tierData.TryAdvanceTier())
        {
            matterManager.ApplyUpgrades(tierData.CurrentTierData);
            upgradeManager.ResetUpgrades();
            Debug.Log($"Matter advanced to Tier {tierData.currentTier + 1}");
        }
        else
        {
            Debug.Log("Already at highest Matter Tier!");
        }
    }
}
