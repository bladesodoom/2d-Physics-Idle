using UnityEngine;

public class CurrencyManager : Manager<CurrencyManager>
{
    public double currentMoney { get; private set; }

    public void InitializeStats(SaveData saveData)
    {
        currentMoney = saveData.money;
    }

    public void AddMoney(double amount)
    {
        currentMoney += amount;
    }

    public void RemoveMoney(double amount)
    {
        if (!HasEnoughMoney(amount))
        {
            Debug.LogWarning("Not enough money to remove!");
        }
        currentMoney -= amount;
    }

    private bool HasEnoughMoney(double amount)
    {
        return currentMoney >= amount;
    }
}
