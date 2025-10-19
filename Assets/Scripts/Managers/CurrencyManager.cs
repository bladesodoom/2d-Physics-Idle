using System;

using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public float currentCurrency = 0;

    public event Action OnCurrencyChanged;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCurrency(float amount)
    {
        currentCurrency += amount;
        OnCurrencyChanged?.Invoke();
    }

    public bool TrySpend(float amount)
    {
        if (currentCurrency < amount) return false;
        currentCurrency -= amount;
        OnCurrencyChanged?.Invoke();
        return true;
    }
}
