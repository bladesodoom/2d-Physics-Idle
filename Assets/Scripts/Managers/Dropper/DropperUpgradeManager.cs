using UnityEngine;

public class DropperUpgradeManager : MonoBehaviour
{
    public static DropperUpgradeManager Instance { get; private set; }

    [Header("References")]
    public DropperManager dropperManager;

    [Header("Upgrade Levels")]
    public int minTierLevel = 0;
    public int maxTierLevel = 0;
    public int spawnRateLevel = 0;
    public int spawnCountLevel = 0;
    public int valueBonusLevel = 0;
    public int widthLevel = 0;

    [Header("Base Costs")]
    public float baseCost = 10f;
    public float costGrowth = 2f;

    [Header("Upgrade Growth Values")]
    public float spawnRateGrowth = 1.15f;
    public float spawnCountGrowth = 1.10f;
    public float valueBonusGrowth = 1.25f;
    public float widthGrowth = 1.10f;
    public int tierGrowth = 1;

    public static event System.Action OnDropperUpgraded;

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

    public void UpgradeMinTier() => TryUpgrade(ref minTierLevel, ApplyMinTierUpgrade);
    public void UpgradeMaxTier() => TryUpgrade(ref maxTierLevel, ApplyMaxTierUpgrade);
    public void UpgradeSpawnRate() => TryUpgrade(ref spawnRateLevel, ApplySpawnRateUpgrade);
    public void UpgradeSpawnCount() => TryUpgrade(ref spawnCountLevel, ApplySpawnCountUpgrade);
    public void UpgradeValueBonus() => TryUpgrade(ref valueBonusLevel, ApplyValueBonusUpgrade);
    public void UpgradeWidth() => TryUpgrade(ref widthLevel, ApplyWidthUpgrade);

    private void TryUpgrade(ref int level, System.Action onUpgrade)
    {
        float cost = GetUpgradeCost(level);

        if (CurrencyManager.Instance.TrySpend(cost))
        {
            level++;
            onUpgrade.Invoke();

            OnDropperUpgraded?.Invoke();

            UIManager.Instance?.UpdateDropperText();
        }
        else
        {
            Debug.Log("Not enough currency to purchase Dropper upgrade!");
        }
    }

    private float GetUpgradeCost(int level)
    {
        return baseCost * Mathf.Pow(costGrowth, level);
    }

    private void ApplyMinTierUpgrade()
    {
        dropperManager.minMatterTier += tierGrowth;
        Debug.Log($"Dropper Min Matter Tier upgraded! Now: {dropperManager.minMatterTier}");
    }

    private void ApplyMaxTierUpgrade()
    {
        dropperManager.maxMatterTier += tierGrowth;
        Debug.Log($"Dropper Max Matter Tier upgraded! Now: {dropperManager.maxMatterTier}");
    }

    private void ApplySpawnRateUpgrade()
    {
        dropperManager.spawnRate /= spawnRateGrowth;
        Debug.Log($"Dropper Spawn Rate upgraded! Now: {dropperManager.spawnRate:F2}s");
    }

    private void ApplySpawnCountUpgrade()
    {
        dropperManager.spawnCount = Mathf.CeilToInt(dropperManager.spawnCount * spawnCountGrowth);
        Debug.Log($"Dropper Spawn Count upgraded! Now: {dropperManager.spawnCount}");
    }

    private void ApplyValueBonusUpgrade()
    {
        dropperManager.additionalMatterValue *= valueBonusGrowth;
        Debug.Log($"Dropper Value Bonus upgraded! Now: {dropperManager.additionalMatterValue:F2}");
    }

    private void ApplyWidthUpgrade()
    {
        dropperManager.dropperWidth *= widthGrowth;
        Debug.Log($"Dropper Width upgraded! Now: {dropperManager.dropperWidth:F2}");
    }

    public void ResetUpgrades()
    {
        minTierLevel = 0;
        maxTierLevel = 0;
        spawnRateLevel = 0;
        spawnCountLevel = 0;
        valueBonusLevel = 0;
        widthLevel = 0;
    }
}
