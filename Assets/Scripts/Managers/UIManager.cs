using TMPro;

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] protected TextMeshProUGUI currencyText;
    [SerializeField] protected TextMeshProUGUI massText;
    [SerializeField] protected TextMeshProUGUI maxMatterText;
    [SerializeField] protected TextMeshProUGUI spawnIntervalText;
    [SerializeField] protected TextMeshProUGUI matterScaleText;
    [SerializeField] protected TextMeshProUGUI baseValueText;
    [SerializeField] protected TextMeshProUGUI matterDamageText;

    private MatterUpgradeManager upgradeManager => MatterUpgradeManager.Instance;
    private MatterManager matterManager => MatterUpgradeManager.Instance.matterManager;

    public void OnClickIncreaseMaxMatter() => MatterUpgradeManager.Instance.UpgradeMaxActiveMatter();
    public void OnClickIncreaseSpawnRate() => MatterUpgradeManager.Instance.UpgradeSpawnInterval();
    public void OnClickDecreaseSize() => MatterUpgradeManager.Instance.UpgradeScale();
    public void OnClickIncreaseValue() => MatterUpgradeManager.Instance.UpgradeBaseValue();
    public void OnClickDecreaseDamage() => MatterUpgradeManager.Instance.UpgradeDamage();

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
        UpdateMatterText();
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
        massText.text = $"Mass: {Blackhole.Instance.currentMass:F2}";
    }

    public void UpdateMatterText()
    {
        maxMatterText.text = FormatUpgradeText(
            "Max Matter",
            upgradeManager.maxActiveMatterLevel,
            upgradeManager.GetUpgradeCost(upgradeManager.maxActiveMatterLevel),
            matterManager.maxActiveMatter,
            matterManager.maxActiveMatter + 5
        );

        spawnIntervalText.text = FormatUpgradeText(
            "Spawn Speed",
            upgradeManager.spawnIntervalLevel,
            upgradeManager.GetUpgradeCost(upgradeManager.spawnIntervalLevel),
            matterManager.spawnInterval,
            matterManager.spawnInterval * 0.95f
        );

        matterScaleText.text = FormatUpgradeText(
            "Scale",
            upgradeManager.scaleLevel,
            upgradeManager.GetUpgradeCost(upgradeManager.scaleLevel),
            matterManager.matterScale,
            matterManager.matterScale * 0.95f
        );

        baseValueText.text = FormatUpgradeText(
            "Base Value",
            upgradeManager.baseValueLevel,
            upgradeManager.GetUpgradeCost(upgradeManager.baseValueLevel),
            matterManager.baseValue,
            matterManager.baseValue * 1.1f
        );

        matterDamageText.text = FormatUpgradeText(
            "Damage",
            upgradeManager.damageLevel,
            upgradeManager.GetUpgradeCost(upgradeManager.damageLevel),
            matterManager.matterDamage,
            matterManager.matterDamage * 0.95f
        );
    }

    private string FormatUpgradeText(string name, int level, float cost, float currentValue, float nextValue)
    {
        return $"{name} Lv.{level}\nCost: {cost:0.0}\nChange: {currentValue:0.##} > {nextValue:0.##}";
    }
}
