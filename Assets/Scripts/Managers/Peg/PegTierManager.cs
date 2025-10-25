using UnityEngine;

public class PegTierManager : MonoBehaviour
{
    public static PegTierManager Instance { get; private set; }

    [Header("References")]
    public PegManager pegManager;
    public PegUpgradeManager upgradeManager;

    [Header("Tier Data")]
    public PegTierData tierData;

    [Header("Scaling Settings")]
    public int totalTiers = 5;

    [Header("Base Values")]
    public float baseValue = 1f;
    public float valueGrowth = 2.0f;

    public float baseHP = 50f;
    public float hpGrowth = 1.75f;

    public float baseUpgradeCost = 10f;
    public float upgradeCostGrowth = 2f;

    public static event System.Action OnTierAdvanced;

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
            tierData = new PegTierData();

        GenerateTiers();
    }

    private void GenerateTiers()
    {
        tierData.tierList.Clear();

        for (int i = 0; i < totalTiers; i++)
        {
            PegData tier = new PegData
            {
                tierIndex = i,
                baseValue = baseValue * Mathf.Pow(valueGrowth, i),
                maxHP = baseHP * Mathf.Pow(hpGrowth, i),
                upgradeCost = baseUpgradeCost * Mathf.Pow(upgradeCostGrowth, i)
            };

            tierData.tierList.Add(tier);
        }
    }

    public void PromoteTier()
    {
        if (tierData.TryAdvanceTier())
        {
            PegData newTier = tierData.CurrentTierData;
            pegManager.ApplyTierData(newTier);
            upgradeManager.ResetUpgrades();

            OnTierAdvanced?.Invoke();
            Debug.Log($"[PegTierManager] Advanced to Tier {tierData.currentTier + 1}");
        }
        else
        {
            Debug.Log("Already at highest Peg Tier!");
        }
    }
}
