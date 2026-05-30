using UnityEngine;

public class CurrencyManager : Manager<CurrencyManager>
{
    public float currentMoney { get; private set; }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
    }

    public void RemoveMoney(float amount)
    {
        if (!HasEnoughMoney(amount))
        {
            Debug.LogWarning("Not enough money to remove!");
        }
        currentMoney -= amount;
    }

    private bool HasEnoughMoney(float amount)
    {
        return currentMoney >= amount;
    }
}
