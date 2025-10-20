using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PegUpgradeUI : MonoBehaviour
{
    public static PegUpgradeUI Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject panel;
    public TextMeshProUGUI levelText;
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

        closeButton.onClick.AddListener(CloseMenu);
    }

    public void OpenMenu(Peg peg)
    {
        currentPeg = peg;
        UpdateUI();
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        currentPeg = null;
    }

    private void UpdateUI()
    {
        if (currentPeg == null) return;

        levelText.text = $"Level: {currentPeg.pegLevel}";
    }
}
