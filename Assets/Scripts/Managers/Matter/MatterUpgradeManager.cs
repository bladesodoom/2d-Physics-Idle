using System;

using UnityEngine;

public class MatterUpgradeManager : MonoBehaviour
{
    public static MatterUpgradeManager Instance { get; private set; }

    [Header("References")]
    public MatterManager matterManager;

    [Header("Upgrade Config")]
    public float baseCost = 10f;
    public float costMultiplier = 2f;

    [Header("Upgrade Multipliers")]
    public float spawnIntervalMultiplier = 0.9f;
    public float maxActiveMultiplier = 1.2f;
    public float valueMultiplier = 1.25f;
    public float scaleMultiplier = 0.9f;
    public float damageMultiplier = 0.9f;

    [Header("Levels")]
    public int spawnLevel = 0;
    public int maxActiveLevel = 0;
    public int valueLevel = 0;
    public int scaleLevel = 0;
    public int damageLevel = 0;

    public event Action OnMatterStatsChanged;

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

    public void UpgradeSpawnInterval() => TryUpgrade(ref spawnLevel, ApplySpawnUpgrade);
    public void UpgradeMaxActive() => TryUpgrade(ref maxActiveLevel, ApplyMaxActiveUpgrade);
    public void UpgradeValue() => TryUpgrade(ref valueLevel, ApplyValueUpgrade);
    public void UpgradeScale() => TryUpgrade(ref scaleLevel, ApplyScaleUpgrade);
    public void UpgradeDamage() => TryUpgrade(ref damageLevel, ApplyDamageUpgrade);

    private void TryUpgrade(ref int level, Action applyUpgrade)
    {
        float cost = GetUpgradeCost(level);

        if (!CurrencyManager.Instance.TrySpend(cost))
        {
            Debug.Log("Not enough currency to upgrade Matter!");
            return;
        }

        level++;
        applyUpgrade?.Invoke();
        OnMatterStatsChanged?.Invoke();

        UIManager.Instance?.UpdateMatterUpgradeUI();
    }

    private float GetUpgradeCost(int level)
    {
        return baseCost * Mathf.Pow(costMultiplier, level);
    }

    private void ApplySpawnUpgrade()
    {
        matterManager.data.spawnInterval *= spawnIntervalMultiplier;
        matterManager.UpdateAllMatterStats();
    }

    private void ApplyMaxActiveUpgrade()
    {
        matterManager.data.maxActive = Mathf.CeilToInt(matterManager.data.maxActive * maxActiveMultiplier);
        matterManager.UpdateAllMatterStats();
    }

    private void ApplyValueUpgrade()
    {
        matterManager.data.baseValue *= valueMultiplier;
        matterManager.UpdateAllMatterStats();
    }

    private void ApplyScaleUpgrade()
    {
        matterManager.data.scale *= scaleMultiplier;
        matterManager.UpdateAllMatterStats();

    }

    private void ApplyDamageUpgrade()
    {
        matterManager.data.damage *= damageMultiplier;
        matterManager.UpdateAllMatterStats();
    }

    public void ResetUpgrades()
    {
        spawnLevel = 0;
        maxActiveLevel = 0;
        valueLevel = 0;
        scaleLevel = 0;
        damageLevel = 0;
    }
}
