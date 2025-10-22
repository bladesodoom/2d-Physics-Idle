using System;

using UnityEngine;

public class PegUpgradeManager : MonoBehaviour
{
    public static PegUpgradeManager Instance { get; private set; }

    [Header("References")]
    public PegManager pegManager;

    [Header("Upgrade Config")]
    public int baseCost = 1;
    public int costMultiplier = 2;

    [Header("Current Levels")]
    public int valueLevel = 0;
    public int hpLevel = 0;

    public Peg currentPeg;

    public event Action OnPegStatsChanged;

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
    public void OpenMenu(Peg peg)
    {
        if (currentPeg != null)
        {
            currentPeg.OnStatsChanged -= HandlePegStatsChanged;
        }
        currentPeg = peg;
        currentPeg.OnStatsChanged += HandlePegStatsChanged;

        PegManager.Instance.pegUpgradeMenu.SetActive(true);
        UIManager.Instance.UpdatePegText();
    }

    private void HandlePegStatsChanged()
    {
        OnPegStatsChanged?.Invoke();
    }

    public void UpgradeValue()
    {
        TryPurchaseUpgrade(ref valueLevel, ApplyValueUpgrade);
    }

    public void UpgradeHealth()
    {
        TryPurchaseUpgrade(ref hpLevel, ApplyHealthUpgrade);
    }

    private void TryPurchaseUpgrade(ref int level, System.Action onUpgrade)
    {
        int cost = GetUpgradeCost(level);
        if (currentPeg.TryUpgrade(cost))
        {
            level++;
            onUpgrade.Invoke();

            HandlePegStatsChanged();
        }
    }

    public int GetUpgradeCost(int level)
    {
        return ((int)(baseCost * Math.Pow(costMultiplier, level)));
    }

    public void ApplyValueUpgrade()
    {
        currentPeg.pegValue *= 1.2f;
    }

    public void ApplyHealthUpgrade()
    {
        currentPeg.maxHP *= 1.5f;
    }
}
