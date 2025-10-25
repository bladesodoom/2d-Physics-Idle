using System.Collections.Generic;

using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [Header("Currency Types")]
    public List<CurrencyData> allCurrencies = new();

    private Dictionary<string, CurrencyData> currencyLookup = new();

    public static event System.Action<string, double> OnCurrencyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeCurrencies();
    }

    private void InitializeCurrencies()
    {
        if (allCurrencies.Count == 0)
        {
            allCurrencies.Add(new CurrencyData("Money", "$"));
            allCurrencies.Add(new CurrencyData("Singularity Points", "SP"));
            allCurrencies.Add(new CurrencyData("Ascension Tokens", "AT"));
            allCurrencies.Add(new CurrencyData("Evolution Essence", "EE"));
        }

        currencyLookup.Clear();
        foreach (var c in allCurrencies)
        {
            if (!currencyLookup.ContainsKey(c.currencyName))
                currencyLookup.Add(c.currencyName, c);
        }
    }

    public void Add(string currencyName, double amount)
    {
        if (!currencyLookup.TryGetValue(currencyName, out CurrencyData data))
        {
            Debug.LogWarning($"Currency '{currencyName}' not found!");
            return;
        }

        data.amount += amount;
        OnCurrencyChanged?.Invoke(currencyName, data.amount);
    }

    public bool TrySpend(string currencyName, double cost)
    {
        if (!currencyLookup.TryGetValue(currencyName, out CurrencyData data))
            return false;

        if (data.amount >= cost)
        {
            data.amount -= cost;
            OnCurrencyChanged?.Invoke(currencyName, data.amount);
            return true;
        }

        return false;
    }

    public double Get(string currencyName)
    {
        if (currencyLookup.TryGetValue(currencyName, out CurrencyData data))
            return data.amount;

        Debug.LogWarning($"Currency '{currencyName}' not found!");
        return 0;
    }

    public bool TrySpend(double cost) => TrySpend("Money", cost);
    public void Add(double amount) => Add("Money", amount);

    public CurrencyData GetCurrency(string name)
    {
        currencyLookup.TryGetValue(name, out CurrencyData data);
        return data;
    }

    public string GetFormatted(string name)
    {
        var c = GetCurrency(name);
        return c != null ? c.GetFormattedAmount() : "0";
    }
}
