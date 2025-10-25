using System;

using UnityEngine;

[Serializable]
public class CurrencyData
{
    public string currencyName;
    public double amount;
    public string symbol;
    public Color displayColor = Color.white;

    public CurrencyData(string name, string symbol, double startingAmount = 0)
    {
        this.currencyName = name;
        this.symbol = symbol;
        this.amount = startingAmount;
    }

    public string GetFormattedAmount()
    {
        return $"{FormatNumber(amount)}{symbol}";
    }

    public static string FormatNumber(double value)
    {
        if (value < 1_000) return value.ToString("F0");
        if (value < 1_000_000) return (value / 1_000d).ToString("F1") + "K";
        if (value < 1_000_000_000) return (value / 1_000_000d).ToString("F1") + "M";
        if (value < 1_000_000_000_000) return (value / 1_000_000_000d).ToString("F1") + "B";

        string[] suffixes = { "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "De" };
        int index = -1;
        double reduced = value;

        while (reduced >= 1000 && index < suffixes.Length - 1)
        {
            reduced /= 1000;
            index++;
        }

        return $"{reduced:F1}{suffixes[Mathf.Max(0, index)]}";
    }
}
