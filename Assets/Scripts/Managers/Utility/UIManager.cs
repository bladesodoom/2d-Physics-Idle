using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // TODO: Implement - This class will manage all UI elements in the game,
    // such as menus, HUD, and popups. It will provide methods to show/hide
    // different UI panels and update UI elements based on game state changes.
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI currencyText;

    public void DoStart()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }
}
