using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Info Text")]
    [SerializeField] private TMP_Text collapseText;
    [SerializeField] private TMP_Text ascendText;
    [SerializeField] private TMP_Text evolutionText;

    [Header("Currency UI Elements")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text singularityText;
    [SerializeField] private TMP_Text ascensionText;
    [SerializeField] private TMP_Text essenceText;

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
    }

    private void OnDisable()
    {
        CurrencyManager.OnCurrencyChanged -= HandleCurrencyChanged;
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

    public void RefreshAllUI()
    {
        UpdateAllCurrencyTexts();
    }
}
