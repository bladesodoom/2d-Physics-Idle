using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public float currentMoney { get; private set; }

    public void DoStart()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void InitializeStats(float saveMoney)
    {
        currentMoney = saveMoney;
    }

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
