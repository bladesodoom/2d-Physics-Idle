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
    [SerializeField] protected TextMeshProUGUI pegLevelText;
    [SerializeField] protected TextMeshProUGUI pegXPText;
    [SerializeField] protected TextMeshProUGUI pegValueText;
    [SerializeField] protected TextMeshProUGUI pegHPText;

    private MatterUpgradeManager matterUpgradeManager => MatterUpgradeManager.Instance;
    private MatterManager matterManager => MatterUpgradeManager.Instance.matterManager;

    private PegUpgradeManager pegUpgradeManager => PegUpgradeManager.Instance;
    private PegManager pegManager => PegUpgradeManager.Instance.pegManager;

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
            "Max",
            matterUpgradeManager.maxActiveMatterLevel,
            matterUpgradeManager.GetUpgradeCost(matterUpgradeManager.maxActiveMatterLevel),
            matterManager.maxActiveMatter,
            matterManager.maxActiveMatter + 5
        );

        spawnIntervalText.text = FormatUpgradeText(
            "Spawn",
            matterUpgradeManager.spawnIntervalLevel,
            matterUpgradeManager.GetUpgradeCost(matterUpgradeManager.spawnIntervalLevel),
            matterManager.spawnInterval,
            matterManager.spawnInterval * 0.95f
        );

        matterScaleText.text = FormatUpgradeText(
            "Size",
            matterUpgradeManager.scaleLevel,
            matterUpgradeManager.GetUpgradeCost(matterUpgradeManager.scaleLevel),
            matterManager.matterScale,
            matterManager.matterScale * 0.95f
        );

        baseValueText.text = FormatUpgradeText(
            "Value",
            matterUpgradeManager.baseValueLevel,
            matterUpgradeManager.GetUpgradeCost(matterUpgradeManager.baseValueLevel),
            matterManager.baseValue,
            matterManager.baseValue * 1.1f
        );

        matterDamageText.text = FormatUpgradeText(
            "Damage",
            matterUpgradeManager.damageLevel,
            matterUpgradeManager.GetUpgradeCost(matterUpgradeManager.damageLevel),
            matterManager.matterDamage,
            matterManager.matterDamage * 0.95f
        );
    }

    public void UpdatePegText()
    {
        pegLevelText.text = $"Level: {pegUpgradeManager.currentPeg}";
        pegXPText.text = $"XP: {pegUpgradeManager.currentPeg.pegCurrentXP} / {pegUpgradeManager.currentPeg.pegXPNextLevel}";

        pegValueText.text = FormatUpgradeText(
            "Value",
            pegUpgradeManager.currentPeg.pegLevel,
            pegUpgradeManager.currentPeg.pegUpgradeCost,
            pegUpgradeManager.currentPeg.pegValue,
            pegUpgradeManager.currentPeg.pegValue * 1.2f
        );

        pegHPText.text = FormatUpgradeText(
            "HP",
            pegUpgradeManager.currentPeg.pegLevel,
            pegUpgradeManager.currentPeg.pegUpgradeCost,
            pegUpgradeManager.currentPeg.maxHP,
            pegUpgradeManager.currentPeg.maxHP * 1.5f
        );
    }

    private string FormatUpgradeText(string name, int level, float cost, float currentValue, float nextValue)
    {
        return $"{name} Level: {level}\nCost: {cost:0.0}\n{currentValue:0.##} > {nextValue:0.##}";
    }
}
