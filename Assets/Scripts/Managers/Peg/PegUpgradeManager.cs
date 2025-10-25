using UnityEngine;

public class PegUpgradeManager : MonoBehaviour
{
    public static PegUpgradeManager Instance { get; private set; }

    [Header("References")]
    public PegManager pegManager;

    [Header("Upgrade Data")]
    public int valueLevel = 0;
    public int hpLevel = 0;

    [Header("Base Costs")]
    public float baseCost = 10f;
    public float costGrowth = 2f;

    [Header("Upgrade Multipliers")]
    public float valueGrowth = 1.15f;
    public float hpGrowth = 1.25f;

    private PegTierManager tierManager => PegTierManager.Instance;

    public static event System.Action OnPegUpgraded;
    public static event System.Action OnPegQuantityChanged;


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

    public void UpgradeValue()
    {
        TryUpgrade(ref valueLevel, ApplyValueUpgrade);
    }

    public void UpgradeHP()
    {
        TryUpgrade(ref hpLevel, ApplyHPUpgrade);
    }

    private void TryUpgrade(ref int level, System.Action onUpgrade)
    {
        float cost = GetUpgradeCost(level);

        if (CurrencyManager.Instance.TrySpend(cost))
        {
            level++;
            onUpgrade.Invoke();

            UIManager.Instance?.UpdatePegText();
        }
        else
        {
            Debug.Log("Not enough currency to purchase upgrade!");
        }
    }

    private float GetUpgradeCost(int level)
    {
        return baseCost * Mathf.Pow(costGrowth, level);
    }

    public void ResetUpgrades()
    {
        valueLevel = 0;
        hpLevel = 0;
    }
    private void ApplyValueUpgrade()
    {
        PegData data = tierManager.tierData.CurrentTierData;
        data.baseValue *= valueGrowth;

        foreach (Peg peg in pegManager.allPegs)
            peg.pegValue = data.baseValue;

        Debug.Log($"Peg value upgraded! New base: {data.baseValue:F2}");
    }

    private void ApplyHPUpgrade()
    {
        PegData data = tierManager.tierData.CurrentTierData;
        data.maxHP *= hpGrowth;

        foreach (Peg peg in pegManager.allPegs)
            peg.maxHP = data.maxHP;

        Debug.Log($"Peg HP upgraded! New max: {data.maxHP:F2}");
    }
