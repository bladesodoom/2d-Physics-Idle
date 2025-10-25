using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Currency UI Elements")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text singularityText;
    [SerializeField] private TMP_Text ascensionText;
    [SerializeField] private TMP_Text essenceText;

    [Header("System Info UI Elements")]
    [SerializeField] private TMP_Text pegInfoText;
    [SerializeField] private TMP_Text dropperInfoText;
    [SerializeField] private TMP_Text dividerInfoText;

    private Dictionary<string, TMP_Text> currencyTextLookup = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        RegisterCurrencyTexts();
    }

    private void OnEnable()
    {
        CurrencyManager.OnCurrencyChanged += HandleCurrencyChanged;
        PegUpgradeManager.OnPegUpgraded += UpdatePegText;
        DropperUpgradeManager.OnDropperUpgraded += UpdateDropperText;
        DividerUpgradeManager.OnDividerUpgraded += UpdateDividerText;
    }

    private void OnDisable()
    {
        CurrencyManager.OnCurrencyChanged -= HandleCurrencyChanged;
        PegUpgradeManager.OnPegUpgraded -= UpdatePegText;
        DropperUpgradeManager.OnDropperUpgraded -= UpdateDropperText;
        DividerUpgradeManager.OnDividerUpgraded -= UpdateDividerText;
    }

    private void RegisterCurrencyTexts()
    {
        currencyTextLookup.Clear();

        if (moneyText) currencyTextLookup["Money"] = moneyText;
        if (singularityText) currencyTextLookup["Singularity Points"] = singularityText;
        if (ascensionText) currencyTextLookup["Ascension Tokens"] = ascensionText;
        if (essenceText) currencyTextLookup["Evolution Essence"] = essenceText;

        UpdateAllCurrencyTexts();
    }

    private void HandleCurrencyChanged(string name, double value)
    {
        if (currencyTextLookup.TryGetValue(name, out TMP_Text text))
        {
            var formatted = CurrencyManager.Instance.GetFormatted(name);
            text.text = $"{name}: {formatted}";
        }
    }

    private void UpdateAllCurrencyTexts()
    {
        foreach (var kvp in currencyTextLookup)
        {
            string name = kvp.Key;
            TMP_Text text = kvp.Value;
            text.text = $"{name}: {CurrencyManager.Instance.GetFormatted(name)}";
        }
    }

    public void UpdatePegText()
    {
        if (!pegInfoText) return;

        var tier = PegTierManager.Instance.tierData.CurrentTierData;
        pegInfoText.text =
            $"Peg Tier {tier.tierIndex + 1}\n" +
            $"Value: {tier.baseValue:F2}\n" +
            $"HP: {tier.maxHP:F0}";
    }

    public void UpdateDropperText()
    {
        if (!dropperInfoText) return;

        var dropper = DropperManager.Instance;
        dropperInfoText.text =
            $"Dropper\n" +
            $"Rate: {dropper.spawnRate:F2}s\n" +
            $"Count: {dropper.spawnCount}\n" +
            $"Value +{dropper.additionalMatterValue:F1}";
    }

    public void UpdateDividerText()
    {
        if (!dividerInfoText) return;

        var dividerUp = DividerUpgradeManager.Instance;
        int total = DividerManager.Instance.baseDividerCount + dividerUp.dividerCountBonus;
        dividerInfoText.text =
            $"Dividers: {total}\n" +
            $"Multiplier: x{dividerUp.valueMultiplier:F2}";
    }

    public void RefreshAllUI()
    {
        UpdateAllCurrencyTexts();
        UpdatePegText();
        UpdateDropperText();
        UpdateDividerText();
    }
}
