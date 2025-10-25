using UnityEngine;

public class ConveyorUpgradeManager : MonoBehaviour
{
    public static ConveyorUpgradeManager Instance { get; private set; }

    [Header("References")]
    public ConveyorManager conveyorManager;

    [Header("Upgrade Settings")]
    public float speedGrowth = 1.1f;
    public float intervalReduction = 0.9f;
    public int level = 0;
    public float baseCost = 50f;
    public float costGrowth = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpgradeConveyor()
    {
        float cost = baseCost * Mathf.Pow(costGrowth, level);
        if (!CurrencyManager.Instance.TrySpend("Money", cost))
        {
            Debug.Log("Not enough currency!");
            return;
        }

        level++;
    }
}
