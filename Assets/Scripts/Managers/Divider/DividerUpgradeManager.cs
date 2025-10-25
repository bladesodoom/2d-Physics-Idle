using UnityEngine;

public class DividerUpgradeManager : MonoBehaviour
{
    public static DividerUpgradeManager Instance { get; private set; }

    [Header("References")]
    public DividerManager dividerManager;

    [Header("Levels")]
    public int dividerCountLevel = 0;
    public int multiplierLevel = 0;

    [Header("Bonuses")]
    public int dividerCountBonus = 0;
    public float valueMultiplier = 1f;

    [Header("Costs")]
    public float baseCost = 25f;
    public float costGrowth = 2f;
    public int dividerCountGrowth = 1;
    public float multiplierGrowth = 1.15f;

    public static event System.Action OnDividerUpgraded;

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

    public void UpgradeDividerCount() => TryUpgrade(ref dividerCountLevel, ApplyDividerCountUpgrade);
    public void UpgradeMultiplier() => TryUpgrade(ref multiplierLevel, ApplyMultiplierUpgrade);

    private void TryUpgrade(ref int level, System.Action onUpgrade)
    {
        float cost = baseCost * Mathf.Pow(costGrowth, level);

        if (CurrencyManager.Instance.TrySpend(cost))
        {
            level++;
            onUpgrade.Invoke();
            OnDividerUpgraded?.Invoke();
            UIManager.Instance?.UpdateDividerText();
        }
        else
        {
            Debug.Log("Not enough currency for Divider upgrade!");
        }
    }

    private void ApplyDividerCountUpgrade()
    {
        dividerCountBonus += dividerCountGrowth;
        Debug.Log($"Divider count bonus now {dividerCountBonus}");
    }

    private void ApplyMultiplierUpgrade()
    {
        valueMultiplier *= multiplierGrowth;
        Debug.Log($"Divider multiplier now {valueMultiplier:F2}x");
    }

    public void ResetUpgrades()
    {
        dividerCountLevel = 0;
        multiplierLevel = 0;
        dividerCountBonus = 0;
        valueMultiplier = 1f;
    }
}
