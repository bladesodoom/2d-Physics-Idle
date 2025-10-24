using System;

using UnityEngine;

public class MatterUpgradeManager : MonoBehaviour
{
    public static MatterUpgradeManager Instance { get; private set; }

    [Header("References")]
    public MatterManager matterManager;

    [Header("Upgrade Costs")]
    [SerializeField] private int baseCost = 10;
    [SerializeField] private float costMultiplier = 1.5f;

    public event Action OnMatterUpgraded;

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

    public void UpgradeValue()
    {
        TryUpgrade(ref matterManager.data.valueLevel, () =>
        {
            matterManager.data.baseValue *= 1.1f;
        });
    }

    public void UpgradeDamage()
    {
        TryUpgrade(ref matterManager.data.damageLevel, () =>
        {
            matterManager.data.damage *= 1.1f;
        });
    }

    public void UpgradeScale()
    {
        TryUpgrade(ref matterManager.data.scaleLevel, () =>
        {
            matterManager.data.scale *= 0.95f;
        });
    }

    public void UpgradeSpawnRate()
    {
        TryUpgrade(ref matterManager.data.spawnRateLevel, () =>
        {
            matterManager.data.spawnInterval *= 0.95f;
        });
    }

    public void UpgradeMaxActive()
    {
        TryUpgrade(ref matterManager.data.maxMatterLevel, () =>
        {
            matterManager.data.maxActiveMatter += 5;
        });
    }

    private void TryUpgrade(ref int level, Action upgradeAction)
    {
        int cost = GetUpgradeCost(level);
        if (CurrencyManager.Instance.TrySpend(cost))
        {
            level++;
            upgradeAction.Invoke();
            OnMatterUpgraded?.Invoke();
        }
    }

    private int GetUpgradeCost(int level)
    {
        return Mathf.RoundToInt(baseCost * Mathf.Pow(costMultiplier, level));
    }
}
