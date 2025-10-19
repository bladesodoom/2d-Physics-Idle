using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PegUpgradeUI : MonoBehaviour
{
    public static PegUpgradeUI Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject panel;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI boostText;
    public Button upgradeButton;
    public Button closeButton;

    private Peg currentPeg;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        panel.SetActive(true);

        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        closeButton.onClick.AddListener(CloseMenu);
    }

    public void OpenMenu(Peg peg)
    {
        currentPeg = peg;
        UpdateUI();
    }

    private void UpgradePeg()
    {
        if (CurrencyManager.Instance.TrySpend(currentPeg.pegUpgradeCost))
        {
            currentPeg.pegLevel++;
            currentPeg.pegUpgradeCost *= 1.5f;
            UpdateUI();
        }
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        currentPeg = null;
    }

    private void OnUpgradeClicked()
    {
        if (currentPeg == null) return;

        if (currentPeg.TryUpgrade())
        {
            UpdateUI();
        }
        return;
    }

    private void UpdateUI()
    {
        if (currentPeg == null) return;

        levelText.text = $"Level: {currentPeg.pegLevel}";
        upgradeCostText.text = $"Upgrade Cost: {currentPeg.pegUpgradeCost:F2}";
    }
}
