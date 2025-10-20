using System;

using UnityEngine;

public class MatterUpgradeManager : MonoBehaviour
{
    public static MatterUpgradeManager Instance { get; private set; }

    [Header("References")]
    public MatterManager matterManager;
    public CurrencyManager currencyManager;

    [Header("Upgrade Config")]
    public float baseCost = 10f;
    public float costMultiplier = 1.25f;

    [Header("Current Levels")]
    public int maxActiveMatterLevel = 0;
    public int spawnIntervalLevel = 0;
    public int scaleLevel = 0;
    public int baseValueLevel = 0;
    public int damageLevel = 0;

    public event Action OnStatsChanged;

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

    public void UpgradeMaxActiveMatter()
    {
        TryPurchaseUpgrade(ref maxActiveMatterLevel, ApplyMaxActiveMatterUpgrade);
        OnStatsChanged?.Invoke();
    }

    public void UpgradeSpawnInterval()
    {
        TryPurchaseUpgrade(ref spawnIntervalLevel, ApplySpawnIntervalUpgrade);
        OnStatsChanged?.Invoke();
    }

    public void UpgradeScale()
    {
        TryPurchaseUpgrade(ref scaleLevel, ApplyScaleUpgrade);
        OnStatsChanged?.Invoke();
    }

    public void UpgradeBaseValue()
    {
        TryPurchaseUpgrade(ref baseValueLevel, ApplyBaseValueUpgrade);
        OnStatsChanged?.Invoke();
    }

    public void UpgradeDamage()
    {
        TryPurchaseUpgrade(ref damageLevel, ApplyDamageUpgrade);
        OnStatsChanged?.Invoke();
    }

    private void TryPurchaseUpgrade(ref int level, System.Action onUpgrade)
    {
        float cost = GetUpgradeCost(level);
        if (currencyManager.TrySpend(cost))
        {
            level++;
            onUpgrade.Invoke();

            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdateMatterText();
            }
        }
    }

    public float GetUpgradeCost(int level)
    {
        return baseCost * Mathf.Pow(costMultiplier, level);
    }

    #region Upgrade Effects

    private void ApplyMaxActiveMatterUpgrade()
    {
        matterManager.maxActiveMatter += 5;
    }

    private void ApplySpawnIntervalUpgrade()
    {
        matterManager.spawnInterval *= 0.95f;
    }

    private void ApplyScaleUpgrade()
    {
        matterManager.matterScale *= 0.95f;
    }

    private void ApplyBaseValueUpgrade()
    {
        matterManager.baseValue *= 1.1f;
    }

    private void ApplyDamageUpgrade()
    {
        matterManager.matterDamage *= 0.95f;
    }

    #endregion
}
