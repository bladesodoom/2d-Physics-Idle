using TMPro;

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI collapseText;
    [SerializeField] private TextMeshProUGUI massText;

    public void OnClickUpgradeMaxMatter() => MatterUpgradeManager.Instance.UpgradeMaxActiveMatter();
    public void OnClickUpgradeSpawn() => MatterUpgradeManager.Instance.UpgradeSpawnInterval();
    public void OnClickUpgradeScale() => MatterUpgradeManager.Instance.UpgradeScale();
    public void OnClickUpgradeValue() => MatterUpgradeManager.Instance.UpgradeBaseValue();
    public void OnClickUpgradeDamage() => MatterUpgradeManager.Instance.UpgradeDamage();

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

    private void OnEnable()
    {
        Blackhole.Instance.OnStatsChanged += UpdateBlackholeStats;
        CurrencyManager.Instance.OnCurrencyChanged += UpdateCurrencyText;
    }

    private void OnDisable()
    {
        Blackhole.Instance.OnStatsChanged -= UpdateBlackholeStats;
        CurrencyManager.Instance.OnCurrencyChanged -= UpdateCurrencyText;
    }

    private void UpdateCurrencyText()
    {
        currencyText.text = $"Currency: {CurrencyManager.Instance.currentCurrency:F2}";
    }

    private void UpdateBlackholeStats()
    {
        collapseText.text = $"Collapse: {Blackhole.Instance.collapseCount}";
        massText.text = $"Mass: {Blackhole.Instance.currentMass:F2}";
    }
}
